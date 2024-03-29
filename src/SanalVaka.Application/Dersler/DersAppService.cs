﻿using SanalVaka.Bolumler;
using SanalVaka.DersDtos;
using SanalVaka.Ogrenciler;
using SanalVaka.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Volo.Abp.Linq;
using System.Linq.Dynamic.Core;
using SanalVaka.Siniflar;
using Polly;
using Microsoft.Extensions.Configuration;
using SanalVaka.OgrenciDtos;
using System.Data.SqlClient;
using SanalVaka.SinifDtos;
using Volo.Abp.Uow;
using SanalVaka.Many2Many;

namespace SanalVaka.Dersler
{
    public class DersAppService:CrudAppService<Ders,DersDto, Guid, PagedAndSortedResultRequestDto,CreateUpdateDersDto>,IDersAppService
    {
        private readonly IRepository<IdentityUser, Guid> _kullaniciRepo;
        private readonly IRepository<Ogrenci> _ogrenciRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Bolum,Guid> _bolumRepository;
        private readonly IRepository<Sinif, Guid> _sinifRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<DersOgrenci,int> _dersOgrenciRepo;
        public DersAppService(IRepository<Ders, Guid> repository,
            IRepository<IdentityUser, Guid> kullaniciRepo,
            ICurrentUser currentUser,
            IRepository<Ogrenci> ogrenciRepo,
            IRepository<Bolum, Guid> bolumRepo,
            IRepository<Sinif, Guid> sinifRepo,
            IUnitOfWorkManager uow,
            IRepository<DersOgrenci,int> dersOgrenci)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Dersler.Default;
            GetListPolicyName = SanalVakaPermissions.Dersler.Default;
            CreatePolicyName = SanalVakaPermissions.Dersler.Create;
            UpdatePolicyName = SanalVakaPermissions.Dersler.Edit;
            DeletePolicyName = SanalVakaPermissions.Dersler.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = currentUser;
            _ogrenciRepo= ogrenciRepo;  
            _bolumRepository = bolumRepo;
            _sinifRepository = sinifRepo;
            _unitOfWorkManager = uow;
            _dersOgrenciRepo = dersOgrenci;
        }
        public async Task<List<DersInfoDto>> GetDersInfo(int skipCount,
        int maxResultCount,
        string sorting="DersAdi",
        string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();

            var entity = queryable.WhereIf(
            !filter.IsNullOrWhiteSpace(),
            Ders => Ders.DersAdi.Contains(filter)
            ).OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount);


            var infoList = new List<DersInfoDto>();
            foreach (var item in entity)
            {
                var res = new DersInfoDto()
                {
                    CreatorId = item.CreatorId,
                    DersAdi = item.DersAdi,
                    DersOnayciAdi = item.DersOnayciAdi,
                    IsOnaylandi = item.IsOnaylandi,
                    BolumId = item.BolumId,
                    BolumName=item.Bolum.BolumAdi,
                    Id = item.Id,
                };
                if (res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;
                }
                infoList.Add(res);
            }
            return infoList;
        }

        public async Task CreateDers(CreateUpdateDersDto input)
        {
            var bolum = await _bolumRepository.FindAsync(input.BolumId);
            if(bolum is null)
            {
                throw new UserFriendlyException("Bölüm bulunamadı");
            }

            var ders = new Ders();
            ders.DersAdi = input.DersAdi;
            ders.BolumId= input.BolumId;
            ders.Bolum = bolum;
            ders.IsOnaylandi = false;

            await Repository.InsertAsync(ders);
        }
        public async Task UpdateDersInfo(UpdateDersDto input)
        {
            var entity = await Repository.GetAsync(input.Id);
            var bolum=await _bolumRepository.GetAsync(input.BolumId);
            if(entity is null)
            {
                throw new UserFriendlyException("Ders bulunamadı");
            }
            if(bolum is null)
            {
                throw new UserFriendlyException("Bölüm bulunamadı");
            }
            //var res = new DersInfoDto();
            //res.BolumId = entity.BolumId;
            //res.BolumName = bolum.BolumAdi;
            //res.Id = entity.Id;
            //res.DersAdi = entity.DersAdi;
            //res.IsOnaylandi = entity.IsOnaylandi;

            entity.BolumId = input.BolumId;
            entity.IsOnaylandi = input.IsOnaylandi;
            entity.Bolum = bolum;
            entity.DersAdi= input.DersAdi;

            await Repository.UpdateAsync(entity);
        }
        public async Task UpdateDers(UpdateDersDto input)
        {
            var entity = await Repository.GetAsync(input.Id);
            if(entity is null)
            {
                throw new UserFriendlyException("Ders Bulunamadı!");
            }
            var yetkiliList = new List<IdentityUser>();
            foreach(var item in input.YetkiliId)
            {
                var yetkili=await _kullaniciRepo.GetAsync(item);
                if(yetkili is null)
                {
                    throw new UserFriendlyException("Yetkili Bulunamadı!");
                }
                yetkiliList.Add(yetkili);
            }
            var bolum=await _bolumRepository.GetAsync(input.BolumId);
            if(bolum is null)
            {
                throw new UserFriendlyException("Bölüm Bulunamadı!");
            }
            entity.Bolum = bolum;
            entity.Yetkililer = yetkiliList;
            entity.DersAdi = input.DersAdi;
            entity.BolumId=input.BolumId;
            
            await Repository.UpdateAsync(entity);

        }
        public async Task OgrenciEkleSingle(Guid guidDers, Guid ogrenciId)
        {
            var entity = await Repository.FindAsync(guidDers);
            if (entity == null)
            {
                throw new UserFriendlyException("Ders bulunamadı!");
            }
            var entityOgrenci = await _ogrenciRepo.GetAsync(x => x.UserId == ogrenciId);


            if (entityOgrenci == null)
            {
                throw new UserFriendlyException("Öğrenci bulunamadı");
            }
            var dersOgrenci = new DersOgrenci();
            dersOgrenci.DersId = entity.Id;
            dersOgrenci.OgrenciId = entityOgrenci.UserId;
            dersOgrenci.IsDeleted = false;
            await _dersOgrenciRepo.InsertAsync(dersOgrenci);
            await Repository.UpdateAsync(entity);
        }
        public async Task OgrenciEkleMulti(List<Guid> list, Guid guidDers)
        {
            var entity = await Repository.FindAsync(guidDers);
            if (entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            var dersOgrenciList = new List<DersOgrenci>();
            foreach (var identityUser in list)
            {
                var ogrenci = await _kullaniciRepo.GetAsync(x => x.Id == identityUser);
                if (ogrenci is not null)
                {
                    var dersOgrenci = new DersOgrenci();
                    dersOgrenci.DersId = entity.Id;
                    dersOgrenci.OgrenciId = ogrenci.Id;
                    dersOgrenciList.Add(dersOgrenci);
                }
            }
            await _dersOgrenciRepo.InsertManyAsync(dersOgrenciList);
            await Repository.UpdateAsync(entity);
        }
        public async Task DersOnayla(Guid guidDers)
        {
            var entity = await Repository.FindAsync(guidDers);

            if (entity == null)
            {
                throw new UserFriendlyException("Ders bulunamadı");
            }
            if (_currentUser.Id is not null)
            {
                var entityKullanici = await _kullaniciRepo.FindAsync((Guid)_currentUser.Id);
                entity.IsOnaylandi = true;
                entity.DersOnayciId = entityKullanici.Id;
                entity.DersOnayciAdi = entityKullanici.Name;
                entity.DersOnayciUsername = entityKullanici.UserName;
                await Repository.UpdateAsync(entity);
            }
            else
            {
                throw new UserFriendlyException("Tekrar giriş yapınız!");
            }
        }
        public async Task YetkiliEkle(Guid dersId,List<Guid> yetkiliId)
        {
            var entity=await Repository.FindAsync(dersId);
            if(entity is null)
            {
                throw new UserFriendlyException("Ders bulunamadı");
            }

            foreach(var item in  yetkiliId)
            {
                var yetkili = await _kullaniciRepo.GetAsync(item);
                if(yetkili is null)
                {
                    throw new UserFriendlyException("Kullanıcı bulunamadı");
                }
                entity.Yetkililer.Add(yetkili);
            }

            await Repository.UpdateAsync(entity);
        }
        public async Task<PagedResultDto<DersInfoDto>> GetPagedDersler(PagedAndSortedResultRequestDto input, string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
            (
                !filter.IsNullOrWhiteSpace(),
                Ders => Ders.DersAdi.Contains(filter)
            )
            //.OrderBy(input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
            var infoList = new List<DersInfoDto>();

            foreach (var item in entity)
            {
                var bolum = await _bolumRepository.GetAsync(item.BolumId);
                if(bolum is null)
                {
                    throw new UserFriendlyException("Bölüm bulunamadı");
                }
                var res = new DersInfoDto()
                {
                    CreatorId = item.CreatorId,
                    DersAdi = item.DersAdi,
                    DersOnayciAdi = item.DersOnayciAdi,
                    IsOnaylandi = item.IsOnaylandi,
                    BolumId = item.BolumId,
                    BolumName = bolum.BolumAdi,
                    Id = item.Id,
                };
                if (res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;

                }
                infoList.Add(res);
            }
            return new PagedResultDto<DersInfoDto>(
                infoList.Count,
                infoList
            );
        }
        public async Task<DersInfoDto> GetDersSingleById(Guid id)
        {
            var entity = await Repository.GetAsync(id);
            var bolum = await _bolumRepository.GetAsync(entity.BolumId);
            var res = new DersInfoDto();
            res.BolumId = entity.BolumId;
            res.BolumName = bolum.BolumAdi;
            res.Id = entity.Id;
            res.DersAdi= entity.DersAdi;    
            res.IsOnaylandi = entity.IsOnaylandi;   

            return res;
        }
        public async Task<List<OgrenciSelectionDto>> GetOgrenciList(Guid dersId)
        {
           // await OgrenciRepoGuncelle();
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT distinct AU.Id,AU.Name,AU.Surname,AU.OgrenciNo FROM AbpUsers AU 
                            WHERE Ogrenci=1 AND NOT EXISTS
			                (
				                SELECT
					                *
				                FROM
					                DersOgrenciler
				                WHERE
					                DersId='{dersId}' AND IsDeleted=0 AND OgrenciId=AU.Id
			                )";
            var OgrenciList = new List<OgrenciSelectionDto>();

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader =await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var ogrenci=new OgrenciSelectionDto();
                        ogrenci.UserId = Guid.Parse(reader["Id"].ToString());
                        ogrenci.OgrenciNo = reader["OgrenciNo"].ToString();
                        ogrenci.OgrenciAdi = reader["Name"].ToString() +" "+ reader["Surname"].ToString();

                        OgrenciList.Add(ogrenci);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }

            return OgrenciList;
        }
        public async Task<List<OgrenciSelectionDto>> GetDersOgrenciList(Guid dersId)
        {
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT DO.OgrenciId as 'OgrenciId',
                            DO.DersId as 'DersId',
                            AU.Name +' '+AU.Surname as 'OgrenciAdi',
                            AU.OgrenciNo as 'OgrenciNo'                         
                            FROM 
                            DersOgrenciler DO 
                            INNER JOIN AbpUsers AU ON AU.Id=DO.OgrenciId
                            WHERE DO.DersId='{dersId}'";
            var OgrenciList = new List<OgrenciSelectionDto>();

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var ogrenci = new OgrenciSelectionDto();
                        ogrenci.UserId = Guid.Parse(reader["OgrenciId"].ToString());
                        ogrenci.OgrenciNo = reader["OgrenciNo"].ToString();
                        ogrenci.OgrenciAdi = reader["OgrenciAdi"].ToString();

                        OgrenciList.Add(ogrenci);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }

            return OgrenciList;
        }
        public async Task<List<SinifInfoDto>> GetDersSiniflar(Guid dersId)
        {
            var entity=await _sinifRepository.GetListAsync(x=>x.DersId==dersId);

            var sinifList=new List<SinifInfoDto>();
            if(entity is null)
            {
                return sinifList;
            }

            foreach (var item in entity)
            {
                var sinif = new SinifInfoDto();

                sinif.DersId = item.DersId;
                sinif.CreatorId = item.CreatorId ?? Guid.Empty;
                sinif.SinifAdi = item.SinifName;
                sinif.SinifLimit=item.SinifLimit;
                sinif.Id = item.Id;
                sinifList.Add(sinif);
            }

            return sinifList;
            
        }
        public async Task OgrenciRepoGuncelle()
        {
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT Id,Name,Surname,OgrenciNo FROM AbpUsers AU WHERE Ogrenci=1";
            var OgrenciList = new List<Ogrenci>();
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var entity = await _ogrenciRepo.GetListAsync();
                foreach (var item in entity)
                {
                    OgrenciList.Add(item);
                }
                await _ogrenciRepo.HardDeleteAsync(OgrenciList);

                await unitOfWork.CompleteAsync();
            }
            OgrenciList.Clear();
            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                
                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var ogrenci = new Ogrenci();
                        ogrenci.UserId = Guid.Parse(reader["Id"].ToString());
                        ogrenci.OgrenciNo = reader["OgrenciNo"].ToString();
                        OgrenciList.Add(ogrenci);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }
            await _ogrenciRepo.InsertManyAsync(OgrenciList);
        }
        public async Task<List<DersDropDownDto>> GetDersDropdown()
        {
            var entity = await Repository.GetListAsync(x => x.IsDeleted == false);
            var res = new List<DersDropDownDto>();
            if (entity is not null)
            {
                foreach (var item in entity)
                {
                    var dersInfo = new DersDropDownDto();
                    dersInfo.Id = item.Id;
                    dersInfo.DersAdi = item.DersAdi;
                    res.Add(dersInfo);
                }
                return res;
            }
            return res;
        }
        public async Task<List<DersInfoDto>> GetDersAnasayfa()
        {
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT
	                            Id,
	                            DersAdi,
	                            BolumId,
	                            CreationTime,
	                            IsOnaylandi
                            FROM
	                            Dersler
                            WHERE
                                IsDeleted=0
                            ORDER BY
	                            CreationTime asc";
            var dersList = new List<DersInfoDto>();

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var ders = new DersInfoDto()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            BolumId = Guid.Parse(reader["BolumId"].ToString()),
                            DersAdi = reader["DersAdi"].ToString(),
                            IsOnaylandi = Convert.ToBoolean(reader["IsOnaylandi"]),                            
                        };
                        var bolum = await _bolumRepository.FindAsync(ders.BolumId);
                        ders.BolumName = bolum.BolumAdi;

                        dersList.Add(ders);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }
            return dersList;
        }
        public async Task<Boolean> GetIsOgrenci()
        {
            var ogrenciId =  _currentUser.GetId();
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT Ogrenci FROM AbpUsers WHERE Id='{ogrenciId}'";
            var res = false;
            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        res = Convert.ToBoolean(reader[0]);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }

            return res;

        }



    }

}

