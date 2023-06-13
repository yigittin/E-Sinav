using SanalVaka.Dersler;
using SanalVaka.Many2Many;
using SanalVaka.OgrenciDtos;
using SanalVaka.Ogrenciler;
using SanalVaka.SinavDtos;
using SanalVaka.SinifDtos;
using SanalVaka.Siniflar;
using SanalVaka.Sorular;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace SanalVaka.Sinavlar
{
    public class SinavAppService: CrudAppService<Sinav, SinavCrudDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSinavDto>, ISinavAppService
    {
        private readonly IRepository<Soru, Guid> _soruRepository;
        private readonly IRepository<Cevap, Guid> _cevapRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;
        private readonly IRepository<OgrenciSinav, int> _ogrenciSinavRepo;
        private readonly IRepository<OgrenciCevap, int> _ogrenciCevapRepo;
        private readonly IRepository<SoruCevap, Guid> _soruCevapRepo;
        private readonly ICurrentUser _currentUser;

        public SinavAppService(IRepository<Sinav, Guid> repository,
            IRepository<Soru, Guid> soruRepository,
            IRepository<Cevap, Guid> cevapRepository,
            IRepository<Ders, Guid> dersRepository,
            ICurrentUser curUser,
            IRepository<OgrenciSinav, int> ogrenciSinavRepo,
            IRepository<OgrenciCevap, int> ogrenciCevapRepo,
            IRepository<SoruCevap, Guid> soruCevapRepo) :base(repository)
        {
            _soruRepository=soruRepository;
            _cevapRepository=cevapRepository;
            _dersRepository=dersRepository;
            _currentUser = curUser;
            _ogrenciCevapRepo = ogrenciCevapRepo;
            _ogrenciSinavRepo = ogrenciSinavRepo;
            _soruCevapRepo = soruCevapRepo;
        }

        public async Task CreateUpdateSinav(CreateUpdateSinavDto input)
        {
            if(input.Id is null || String.IsNullOrWhiteSpace(input.Id.ToString()))
            {
                var ders = await _dersRepository.GetAsync(input.DersId);
                if(ders is null)
                {
                    throw new UserFriendlyException("Ders bulunamadı");
                }
                var sinav = new Sinav()
                {
                    SinavAdi = input.SinavAdi,
                    Agirlik = input.Agirlik,
                    SinavSure = input.SinavSure,
                    BaslangicTarih = input.BaslangicTarih,
                    DersId = input.DersId,
                    Ders = ders,
                };

                await Repository.InsertAsync(sinav);
            }
            else if(input.Id is not null)
            {
                var sinav=await Repository.GetAsync((Guid)input.Id);
                if(sinav is null)
                {
                    throw new UserFriendlyException("Sınav bulunamadı");
                }
                var ders = await _dersRepository.GetAsync(input.DersId);
                if(ders is null)
                {
                    throw new UserFriendlyException("Ders bulunamadı");
                }

                sinav.DersId = input.DersId;
                sinav.Ders = ders;
                sinav.SinavAdi=input.SinavAdi;
                sinav.SinavSure = input.SinavSure;
                sinav.BaslangicTarih = input.BaslangicTarih;
                sinav.Agirlik = input.Agirlik;

                await Repository.UpdateAsync(sinav);
            }
        }

        public async Task DeleteSinav(Guid id)
        {
            var sinav= await Repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            await Repository.DeleteAsync(sinav);
        }   

        public async Task<List<SinavDto>> GetSinavInfo()
        {
            var entityList = await Repository.GetListAsync();
            List<SinavDto> res = new List<SinavDto>();
            foreach(var item in entityList)
            {
                var ders = await _dersRepository.GetAsync(item.DersId);
                var sinav = new SinavDto()
                {
                    Agirlik=item.Agirlik,
                    SinavAdi=item.SinavAdi,
                    SinavSure=item.SinavSure,
                    DersId=item.DersId,
                    BaslangicTarih=item.BaslangicTarih,
                    Id= item.Id,
                    DersAdi=ders.DersAdi
                };
                res.Add(sinav);
            }
            return res;
        }
        public async Task<CompleteSinavDto> GetSinavDetails(Guid id)
        {
            var sinav = await Repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            var ders=await _dersRepository.GetAsync(sinav.DersId);
            var sinavDto = new SinavDto()
            {
                Agirlik = sinav.Agirlik,
                SinavAdi = sinav.SinavAdi,
                SinavSure = sinav.SinavSure,
                DersId = sinav.DersId,
                BaslangicTarih = sinav.BaslangicTarih,
                Id = sinav.Id,
                DersAdi=ders.DersAdi
            };
            var soru = await _soruRepository.GetListAsync(x => x.SinavId == sinav.Id && x.IsDeleted == false);
            List<SoruDto> soruList = new List<SoruDto>();
            foreach(var item in soru)
            {
                var cevapList = await _cevapRepository.GetListAsync(x => x.SoruId == item.Id && x.IsDeleted==false);
                var cevapDtoList= new List<CevapDto>();
                foreach (var item2 in cevapList)
                {
                    var cevap = new CevapDto()
                    {
                        CevapMetni= item2.CevapMetni,
                        Id=item2.Id,
                        IsDogru=item2.IsDogru
                    };
                    cevapDtoList.Add(cevap);
                }
                item.CevapList = (ICollection<Cevap>)cevapDtoList;
            }
            var res = new CompleteSinavDto()
            {
                Sinav= sinavDto,
                SoruList=soruList
            };
            return res;
        }
        public async Task<PagedResultDto<SinavDto>> GetPagedSiniflar(PagedAndSortedResultRequestDto input,string filter=null)
        {
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
                (
                    !filter.IsNullOrWhiteSpace(), Sinav => Sinav.SinavAdi.Contains(filter)
                )
                .OrderBy(x => x.BaslangicTarih)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            List<SinavDto> res = new List<SinavDto>();
            
            foreach (var item in entity)
            {
                var ders = await _dersRepository.GetAsync(item.DersId);
                var sinav = new SinavDto()
                {
                    Agirlik = item.Agirlik,
                    SinavAdi = item.SinavAdi,
                    SinavSure = item.SinavSure,
                    DersId = item.DersId,
                    BaslangicTarih = item.BaslangicTarih,
                    Id = item.Id,
                    DersAdi=ders.DersAdi
                };
                res.Add(sinav);
            }
            return new PagedResultDto<SinavDto>(
                res.Count,
                res
            );
        }
        public async Task<SinavDto> GetSinavSingle(Guid id)
        {
            var sinav = await Repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            var ders = await _dersRepository.GetAsync(sinav.DersId);
            var res = new SinavDto()
            {
                Agirlik=sinav.Agirlik,
                BaslangicTarih=sinav.BaslangicTarih,
                DersAdi=ders.DersAdi,
                DersId=sinav.DersId,
                Id=sinav.Id,
                SinavAdi=sinav.SinavAdi,
                SinavSure = sinav.SinavSure
            };
            return res;
        }

        public async Task SinavBaslat(Guid sinavId)
        {
            var ogrenciSinavList=await _ogrenciSinavRepo.GetListAsync(x=>x.SinavId==sinavId&&x.OgrenciId==_currentUser.Id);
            OgrenciSinavDto ogrenciSinav = new OgrenciSinavDto();
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT
	                        OS.Id as 'Id',
	                        OS.OgrenciId as 'OgrenciId',
	                        OS.SinavId as 'SinavId',
	                        OS.Baslangic as 'Baslangic',
	                        OS.Bitis as 'Bitis',
	                        OS.IsDeleted as 'IsDeleted'
                        FROM
	                        OgrenciSinavlar OS
                        WHERE 
	                        OS.SinavId='{sinavId}' AND OS.OgrenciId='{_currentUser.Id}'";
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
                        ogrenciSinav.SinavId = Guid.Parse(reader["SinavId"].ToString());
                        ogrenciSinav.OgrenciId = Guid.Parse(reader["OgrenciId"].ToString());
                        ogrenciSinav.Baslangic = DateTime.Parse(reader["Baslangic"].ToString());
                        ogrenciSinav.Bitis = DateTime.Parse(reader["Bitis"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }
            var sinav = await Repository.GetAsync(sinavId);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı!");
            }
            if(ogrenciSinav.SinavId!=Guid.Empty)
            {
                if (ogrenciSinav.Bitis < DateTime.Now)
                {
                    throw new UserFriendlyException("Sınav süreniz dolmuştur");
                }
            }
            else if(ogrenciSinav.SinavId == Guid.Empty)
            {
                var yeniGiris = new OgrenciSinav()
                {
                    OgrenciId= (Guid)_currentUser.Id,
                    Baslangic=DateTime.Now,
                    Bitis=DateTime.Now.AddMinutes(sinav.SinavSure),
                    SinavId=sinavId
                };
                await _ogrenciSinavRepo.InsertAsync(yeniGiris);
            }
        }
        public async Task<OgrenciSinavDto> GetOgrenciSinavSure(Guid sinavId)
        {
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sinav = await Repository.GetAsync(sinavId);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı!");
            }
            var sinavOgrenci = await _ogrenciSinavRepo.GetAsync(x => x.OgrenciId == _currentUser.GetId() && x.SinavId == sinavId);
            var sorular = await _soruRepository.GetListAsync(x => x.SinavId == sinavId && x.IsDeleted == false);
            List<OgrenciCevapDto> ogrenciCevap = new List<OgrenciCevapDto>();
            List<SoruDto> soruList=new List<SoruDto>();
            SinavSoruCevapDto sinavSoruCevapDto = new SinavSoruCevapDto();
            foreach(var item in sorular)
            {
                var cevap = await _cevapRepository.GetListAsync(x => x.SoruId == item.Id && x.IsDeleted == false);
                List<CevapDto> cevapDtoList=new List<CevapDto>();
                foreach(var item2 in cevap)
                {
                    CevapDto cDto = new CevapDto()
                    {
                        Id=item2.Id,
                        SoruId=item.Id,
                        CevapMetni=item2.CevapMetni,
                        IsDogru=item2.IsDogru                        
                    };
                    cevapDtoList.Add(cDto);
                }
                //var ogrenciCevaplar = await _ogrenciCevapRepo.GetAsync(x => x.SoruId == item.Id);
                OgrenciCevapDto newCevapDto = new OgrenciCevapDto();
                var sqlQuery = $@"SELECT
	                                OC.SoruId,
	                                OC.CevapId,
	                                OC.OgrenciId,
	                                OC.OgrenciSinavId,
	                                OC.IsDeleted
                                FROM
	                                OgrenciCevaplar OC
                                WHERE
	                                OC.SoruId='{item.Id}' AND OC.OgrenciId='{_currentUser.Id}'";
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
                            newCevapDto.SoruId = Guid.Parse(reader["SoruId"].ToString()); ;
                            newCevapDto.OgrenciCevapId = Guid.Parse(reader["CevapId"].ToString());
                            newCevapDto.CevapList = cevapDtoList;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new UserFriendlyException("Bir şeyler ters gitti");
                    }
                }
                if (newCevapDto.SoruId == Guid.Empty)
                    newCevapDto = null;
                SoruDto newSoru = new SoruDto()
                {
                    Id=item.Id,
                    CevapList= cevapDtoList,
                    ogrenciCevap= newCevapDto,
                    Puan=item.Puan,
                    SoruMetni=item.SoruMetni,              
                    
                };
                soruList.Add(newSoru);
            }
            sinavSoruCevapDto.SoruList = soruList;
            sinavSoruCevapDto.SinavId = sinavId;
            sinavSoruCevapDto.OgrenciId = (Guid)_currentUser.Id;
            OgrenciSinavDto res = new OgrenciSinavDto()
            {
                Baslangic = sinavOgrenci.Baslangic,
                Bitis = sinavOgrenci.Bitis,
                SinavId = sinavId,
                OgrenciId = _currentUser.GetId(),
                SinavSorular = sinavSoruCevapDto,
                
                
            };
            return res;
        }
        public async Task CevapIsaretle(Guid soruId,Guid cevapId)
        {
            var queryable = await _soruCevapRepo.GetQueryableAsync();
            var entity = queryable.Where(x=>x.SoruId==soruId&&x.OgrenciId==_currentUser.Id).FirstOrDefault();
            if(entity == null)
            {
                var newIsaret = new SoruCevap()
                {
                    OgrenciCevap=cevapId,
                    SoruId=soruId,
                    OgrenciId= (Guid)_currentUser.Id
                };
                await _soruCevapRepo.InsertAsync(newIsaret);
            }
            else if(entity is not null)
            {
                entity.OgrenciId = (Guid)_currentUser.Id;
                entity.SoruId = soruId;
                entity.OgrenciCevap = cevapId;

                await _soruCevapRepo.UpdateAsync(entity);                
            }

            //var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            //var sqlQuery = $@"SELECT DO.OgrenciId as 'OgrenciId',
            //                DO.DersId as 'DersId',
            //                AU.Name +' '+AU.Surname as 'OgrenciAdi',
            //                AU.OgrenciNo as 'OgrenciNo'                         
            //                FROM 
            //                DersOgrenciler DO 
            //                INNER JOIN AbpUsers AU ON AU.Id=DO.OgrenciId
            //                WHERE DO.DersId='{dersId}'";
            //var OgrenciList = new List<OgrenciSelectionDto>();

            //using (SqlConnection connection =
            //new SqlConnection(connectionString))
            //{
            //    // Create the Command and Parameter objects.
            //    SqlCommand command = new SqlCommand(sqlQuery, connection);

            //    // Open the connection in a try/catch block.
            //    // Create and execute the DataReader, writing the result
            //    // set to the console window.
            //    try
            //    {
            //        connection.Open();
            //        SqlDataReader reader = await command.ExecuteReaderAsync();
            //        while (await reader.ReadAsync())
            //        {
            //            var ogrenci = new OgrenciSelectionDto();
            //            ogrenci.UserId = Guid.Parse(reader["OgrenciId"].ToString());
            //            ogrenci.OgrenciNo = reader["OgrenciNo"].ToString();
            //            ogrenci.OgrenciAdi = reader["OgrenciAdi"].ToString();

            //            OgrenciList.Add(ogrenci);
            //        }
            //        reader.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new UserFriendlyException("Bir şeyler ters gitti");
            //    }
            //}

        }
        public async Task<List<SinavDto>> SinavAnasayfa()
        {
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"SELECT
	                            Id,
	                            SinavAdi,
	                            Agirlik,
	                            DersId,
	                            SinavSure,
                                BaslangicTarih
                            FROM
	                            Sinavlar
                            WHERE
	                            IsDeleted=0
                            ORDER BY
	                            BaslangicTarih asc";
            var sinavList = new List<SinavDto>();

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
                        var sinav = new SinavDto()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            SinavAdi = reader["SinavAdi"].ToString(),
                            Agirlik = Convert.ToInt32(reader["Agirlik"]),
                            DersId = Guid.Parse(reader["DersId"].ToString()),
                            BaslangicTarih = DateTime.Parse(reader["BaslangicTarih"].ToString()),
                            SinavSure = Convert.ToInt32(reader["SinavSure"])
                        };
                        var ders = await _dersRepository.FindAsync(sinav.DersId);
                        sinav.DersAdi = ders.DersAdi;

                        sinavList.Add(sinav);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }
            return sinavList;
        }
        public async Task<List<SinavDto>> GetOgrenciSinav()
        {
            var curr = _currentUser.GetId();
            var connectionString = "Server=.;Database=SanalVaka;Trusted_Connection=True;TrustServerCertificate=True";
            var sqlQuery = $@"WITH SINAV AS
                                (SELECT
	                                Id,
	                                SinavAdi,
	                                Agirlik,
	                                DersId,
	                                SinavSure,
	                                BaslangicTarih
                                FROM
	                                Sinavlar
                                WHERE
	                                IsDeleted=0
                                ),
                                DERS as
                                (
	                                SELECT
		                                Id
	                                FROM 
		                                Dersler
	                                WHERE
		                                IsDeleted=0
                                ),
                                DERSOGRENCI as 
                                (
	                                SELECT
		                                *
	                                FROM
	                                DersOgrenciler
                                )
                                SELECT distinct
	                                S.DersId,
	                                S.SinavAdi,
	                                S.Agirlik,
	                                S.SinavSure,
	                                S.BaslangicTarih,
	                                S.Id
                                FROM 
	                                SINAV S
                                INNER JOIN DERS D ON D.Id=S.DersId
                                INNER JOIN DERSOGRENCI DO ON DO.DersId=D.Id
                                WHERE
	                                DO.OgrenciId='{curr}'";
            var sinavList = new List<SinavDto>();

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
                        var sinav = new SinavDto()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            SinavAdi = reader["SinavAdi"].ToString(),
                            Agirlik = Convert.ToInt32(reader["Agirlik"]),
                            DersId = Guid.Parse(reader["DersId"].ToString()),
                            BaslangicTarih = DateTime.Parse(reader["BaslangicTarih"].ToString()),
                            SinavSure = Convert.ToInt32(reader["SinavSure"])
                        };
                        var ders = await _dersRepository.FindAsync(sinav.DersId);
                        sinav.DersAdi = ders.DersAdi;

                        sinavList.Add(sinav);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Bir şeyler ters gitti");
                }
            }
            return sinavList;
        }

        
    }
}
