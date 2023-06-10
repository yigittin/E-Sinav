using SanalVaka.Dersler;
using SanalVaka.Permissions;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using Volo.Abp;
using Volo.Abp.ObjectMapping;
using SanalVaka.DersDtos;
using System.Data.SqlClient;

namespace SanalVaka.Bolumler
{
    public class BolumAppService : CrudAppService<Bolum, BolumDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBolumDto>, IBolumAppService
    {

        private readonly IRepository<IdentityUser, Guid> _kullaniciRepo;
        private readonly ICurrentUser _currentUser;
        public BolumAppService(IRepository<Bolum, Guid> repository, IRepository<IdentityUser, Guid> kullaniciRepo,ICurrentUser curUser)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Bolumler.Default;
            GetListPolicyName = SanalVakaPermissions.Bolumler.Default;
            CreatePolicyName = SanalVakaPermissions.Bolumler.Create;
            UpdatePolicyName = SanalVakaPermissions.Bolumler.Edit;
            DeletePolicyName = SanalVakaPermissions.Bolumler.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = curUser;
        }

        public async Task<List<BolumInfoDto>> GetBolumlerInfo(int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();
                            
                var entity= queryable.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                Bolum => Bolum.BolumAdi.Contains(filter)
                ).OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount);


            var infoList =new List<BolumInfoDto>();
            foreach (var item in entity)
            {
                var res = new BolumInfoDto()
                {
                    CreatorId=item.CreatorId,
                    BolumAdi=item.BolumAdi,
                    BolumOnayciAdi=item.BolumOnayciAdi,
                    IsOnaylandi=item.IsOnaylandi,
                    Id=item.Id,
                };
                if(res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;
                }
                infoList.Add(res);
            }
            return infoList;
        }
        
        public async Task<BolumInfoDto> GetBolumSingle(Guid id)
        {
            var entity = await Repository.GetAsync(id);
            var res = new BolumInfoDto();
            res.BolumAdi = entity.BolumAdi;
            res.IsOnaylandi=entity.IsOnaylandi;
            res.BolumOnayciAdi=entity.BolumOnayciAdi; 
            res.Id=entity.Id;

            return res;
        }
        public async Task<BolumInfoDto> NewBolum(CreateUpdateBolumDto input)
        {
            var bolum = new Bolum();
            bolum.BolumAdi = input.BolumAdi;
            bolum.IsOnaylandi = input.IsOnaylandi;
            await Repository.InsertAsync(bolum);
            var res = new BolumInfoDto()
            {
                BolumAdi = bolum.BolumAdi,
            };
            return res;
        }
        public async Task<PagedResultDto<BolumInfoDto>> GetPagedBolumler(PagedAndSortedResultRequestDto input, string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
            (
                !filter.IsNullOrWhiteSpace(),
                Bolum => Bolum.BolumAdi.Contains(filter)
            )
            //.OrderByIf(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
            var infoList = new List<BolumInfoDto>();

            foreach (var item in entity)
            {
                var res = new BolumInfoDto()
                {
                    CreatorId = item.CreatorId,
                    BolumAdi = item.BolumAdi,
                    BolumOnayciAdi = item.BolumOnayciAdi,
                    IsOnaylandi = item.IsOnaylandi,
                    Id = item.Id,
                };
                if (res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;

                }
                infoList.Add(res);
            }
            return new PagedResultDto<BolumInfoDto>(
                infoList.Count,
                infoList
            );
        }

        public async Task<BolumInfoDto> UpdateBolum(UpdateBolumDto input)
        {
            var entity=await Repository.GetAsync(x=>x.Id==input.Id&&x.IsDeleted==false);
            if (entity == null)
            {
                throw new UserFriendlyException("Bölüm bulunamadı");
            }

            entity.BolumAdi=input.BolumAdi;
            await Repository.UpdateAsync(entity);
            var res = new BolumInfoDto();
            res.BolumAdi=entity.BolumAdi;

            return res;
            
        }
        
        public async Task OnaylaBolum(Guid id)
        {
            var entity=await Repository.GetAsync(x=>x.Id== id && x.IsDeleted == false);
            if (entity == null)
            {
                throw new UserFriendlyException("Bölüm bulunamadı");
            }

            entity.IsOnaylandi=true;
            entity.BolumOnayciId = _currentUser.Id;
            entity.BolumOnayciAdi = _currentUser.Name + " "+_currentUser.SurName;
            entity.BolumOnayciUsername = _currentUser.UserName;

            await Repository.UpdateAsync(entity);
        }

        public async Task<List<BolumDropDownDto>> GetBolumDropdown()
        {
            var entity = await Repository.GetListAsync(x => x.IsDeleted == false);
            if(entity is not null)
            {
                var res=new List<BolumDropDownDto>();
                foreach(var item in entity)
                {
                    var bolumInfo=new BolumDropDownDto();
                    bolumInfo.Id = item.Id;
                    bolumInfo.BolumAdi = item.BolumAdi;
                    res.Add(bolumInfo);
                }
                return res;
            }
            else
            {
                throw new UserFriendlyException("Bölüm listesi boş!");
            }
        }

        public async Task YetkiliAta(Guid id,Guid kullaniciId)
        {
            var entity = await Repository.GetAsync(id);
            if(entity is null)
            {
                throw new UserFriendlyException("Bölüm Bulunamadı");
            }
            var kullanici=await _kullaniciRepo.GetAsync(kullaniciId);
            if(kullanici is null)
            {
                throw new UserFriendlyException("Kullanıcı Bulunamadı");
            }
            entity.Yetkililer.Add(kullanici);
            await Repository.UpdateAsync(entity);
        }
        public async Task<List<BolumInfoDto>> BolumAnasayfa()
        {
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT
	                            Id,
	                            BolumAdi,
	                            IsOnaylandi,
	                            BolumOnayciId,
	                            BolumOnayciAdi,
	                            CreationTime
                            FROM
	                            Bolumler
                            WHERE
                                IsDeleted=0
                            ORDER BY 
	                            CreationTime asc";
            var bolumList = new List<BolumInfoDto>();

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
                        var bolum = new BolumInfoDto()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            BolumAdi = reader["BolumAdi"].ToString(),
                            IsOnaylandi = Convert.ToBoolean(reader["IsOnaylandi"]),
                            BolumOnayciAdi = reader["BolumOnayciAdi"].ToString()
                        };


                        bolumList.Add(bolum);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }
            return bolumList;
        }
        
    }
}
