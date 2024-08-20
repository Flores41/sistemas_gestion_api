using Abstraction.IAplication.Administration;
using Abstraction.IAplication.Masters;
using Abstraction.IAplication.Util;
using Aplication.Administration;
using Aplication.Util;
using BudgetForcast.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using Model;
using Models.Administration;
using Models.Masters;
using NPOI.SS.Formula.Functions;
using System.Linq;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Masters
{
    [Route("Masters/Shedule/")]
    [ApiController]
    [Authorize]

    public class SheduleController : Controller
    {
        private ISheduleAplication iISheduleAplication;
        private IAuxiliaryTablesAplication iIAuxiliaryTablesAplication;
        private IUsersAplication iIUsersAplication;

        private IConfiguration iIConfiguration;

        public SheduleController
         (
            ISheduleAplication _ISheduleAplication,
            IAuxiliaryTablesAplication _IAuxiliaryTablesAplication,
            IUsersAplication _IUsersAplication,

        IConfiguration IConfiguration)
        {
            this.iIAuxiliaryTablesAplication = _IAuxiliaryTablesAplication;
            this.iIUsersAplication = _IUsersAplication;

            this.iISheduleAplication = _ISheduleAplication;
            this.iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListShedule")]
        public async Task<ActionResult> GetListShedule([FromBody] SheduleDto request)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {

                res = await this.iISheduleAplication.GetListShedule(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterShedule")]
        public async Task<ActionResult> RegisterShedule([FromBody] SheduleDto request)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                SheduleDto _request = new SheduleDto
                {
                    iid_shedule_weekly = -1,
                    vname_shedule_weekly = "",
                    vdescription_shedule_weekly = "",
                    iid_week_shedule_weekly = request.iid_week_shedule_weekly,
                    iid_day_shedule_weekly = -1,
                    vhour_shedule_weekly = "",
                    itype_stream = -1,
                    iid_user = request.iid_user_token,
                    iid_comunity = request.iid_comunity,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 100000,
                };
                UserByParameterDto requestUser = new UserByParameterDto
                {
                    iid_user = request.iid_user_token,
                    vchannel_twitch_user  = ""
                };

                var resUser = await this.iIUsersAplication.GetListUserByParameter(requestUser);
                var resSheduleHours = await this.iISheduleAplication.GetListShedule(_request);

                var iHoursTotal = resUser.Item.ihours_normal_range_user + resUser.Item.ihours_vip_range_user;
                if (resSheduleHours.Data.Count >= iHoursTotal)
                {
                    res.Message = "Maximo De Horarios Agendados";
                    res.InnerException = "Alcanzaste el Maximo de Horarios Permitidos en tu Rango.";
                    res.IsSuccess = false;
                    return Ok(res);
                }

                var sheduleExistHour = resSheduleHours.Data.FindAll( p=> p.vhour_shedule_weekly == request.vhour_shedule_weekly 
                                                                      && p.iid_day_shedule_weekly == request.iid_day_shedule_weekly).Count;
                if (sheduleExistHour > 0)
                {
                    res.Message = "Horario Ya Agendado.";
                    res.InnerException = "Selecciona Otra Hora o Dia Distinto.";
                    res.IsSuccess = false;
                    return Ok(res);
                }
                res = await this.iISheduleAplication.RegisterShedule(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteShedule")]
        public async Task<ActionResult> DeleteShedule([FromQuery] int iid_shedule_weekly)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {
                SheduleDto request = new SheduleDto();
                request.iid_shedule_weekly = iid_shedule_weekly;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iISheduleAplication.DeleteShedule(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteSheduleAll")]
        public async Task<ActionResult> DeleteSheduleAll([FromQuery] int iid_shedule_weekly)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {
                SheduleDto request = new SheduleDto();
                request.iid_shedule_weekly = iid_shedule_weekly;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iISheduleAplication.DeleteSheduleAll(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("GetListSheduleByWeek")]
        public async Task<ActionResult> GetListSheduleByWeek([FromBody] SheduleDto request)
        {
            ResultDTO<SheduleWeekDto> res = new ResultDTO<SheduleWeekDto>();
            try
            {
                int iiid_user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                UserByParameterDto requestUser = new UserByParameterDto
                {
                    iid_user = iiid_user,
                    vchannel_twitch_user = ""
                };

                SheduleDto _request = new SheduleDto
                {
                    iid_shedule_weekly = -1,
                    vname_shedule_weekly = "",
                    vdescription_shedule_weekly = "",
                    iid_week_shedule_weekly = request.iid_week_shedule_weekly,
                    iid_day_shedule_weekly = -1,
                    vhour_shedule_weekly = "",
                    itype_stream = -1,
                    iid_user = -1,
                    iid_comunity = request.iid_comunity,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 100000,
                };

                TableDetailDto requestTimeZone = new TableDetailDto
                {
                    iid_table_detail = -1,
                    iid_table_headboard = 7,
                    iid_detail = -1,
                    vdescription = "",
                    vvalue_text_1 = "",
                    vvalue_text_2 = "",
                    vvalue_text_3 = "",
                    vvalue_integer_1 = -1,
                    vvalue_integer_2 = -1,
                    vvalue_integer_3 = -1,
                    vvalue_decimal_1 = -1,
                    vvalue_decimal_2 = -1,
                    vvalue_decimal_3 = -1,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 1000,
                };


                var resUser = await this.iIUsersAplication.GetListUserByParameter(requestUser);
                var resTimeZone = await this.iIAuxiliaryTablesAplication.GetListTableDetail(requestTimeZone);

                _request.iid_user = (bool)resUser.Item.bis_member_user ? iiid_user : -1;


                var resShedule = await this.iISheduleAplication.GetListShedule(_request);


                var itime_mex = resTimeZone.Data.Find(p => p.iid_detail == 1);
                var itime_user = resTimeZone.Data.Find(p => p.iid_detail == resUser.Item.iid_time_zone_user);
                List<ListCbDTO> getTimeCb = Util.GetTimeZone((int)itime_user.vvalue_integer_1, itime_user.vvalue_text_1, (int)itime_mex.vvalue_integer_1);


                List<SheduleWeekDto> shedule_ = getTimeCb.Select(p => new SheduleWeekDto
                {
                    iid_shedule_weekly = p.id,
                    vzone_time_user = p.vvalue2,
                    vzone_time_mx = p.vvalue3,
                    vday_monday = new List<StreamInfoDto>(),
                    vday_tuesday = new List<StreamInfoDto>(),
                    vday_wednesday = new List<StreamInfoDto>(),
                    vday_thursday = new List<StreamInfoDto>(),
                    vday_friday = new List<StreamInfoDto>(),
                    vday_saturday = new List<StreamInfoDto>(),
                }).ToList();

                res.VipAvailable = false;
                res.SheduleAvailable = false;


                if (resShedule.Data.Count > 0)
                {
                    foreach (SheduleDto item in resShedule.Data)
                    {
                        var search_hour = shedule_.Find(p => p.vzone_time_mx == item.vhour_shedule_weekly);
                        if (search_hour != null)
                        {
                            StreamInfoDto streamInfo = new StreamInfoDto
                            {
                                vname_channel = item.itype_stream == 2 ? item.vchannel_twitch + " [VIP]" : item.vchannel_twitch,
                                vurl_channel = item.vurl_channel_twitch,
                                itype_stream = item.itype_stream
                            };


                            switch (item.iid_day_shedule_weekly)
                            {
                                case 1:
                                    search_hour.vday_monday.Add(streamInfo);
                                    break;
                                case 2:
                                    search_hour.vday_tuesday.Add(streamInfo);
                                    break;
                                case 3:
                                    search_hour.vday_wednesday.Add(streamInfo);
                                    break;
                                case 4:
                                    search_hour.vday_thursday.Add(streamInfo);
                                    break;
                                case 5:
                                    search_hour.vday_friday.Add(streamInfo);
                                    break;
                                case 6:
                                    search_hour.vday_saturday.Add(streamInfo);
                                    break;
                            }
                        }
                    }
                }

                var iTotalShedulesByUser = resShedule.Data.FindAll(p => p.iid_user == iiid_user).Count;
                var iVipShedulesByUser = resShedule.Data.FindAll(p => p.iid_user == iiid_user && p.itype_stream == 2).Count;
                var iNormalShedulesByUser = resShedule.Data.FindAll(p => p.iid_user == iiid_user && p.itype_stream == 1).Count;

                int iTotalHoursUser = (int)resUser.Item.ihours_normal_range_user + (int)resUser.Item.ihours_vip_range_user;

                res.NormalAvailable = iNormalShedulesByUser < (int)resUser.Item.ihours_normal_range_user ? true : false;
                res.VipAvailable = iVipShedulesByUser < (int)resUser.Item.ihours_vip_range_user ? true : false;

                res.SheduleAvailable = iTotalShedulesByUser < iTotalHoursUser ? true : false;





                string HoraAgendaUser = resUser.Item.vtime_agenda_range_user;

                DateTime fecha = DateTime.Now.AddHours(-4);//-4
                string HoraActual = fecha.Hour.ToString() + ":" + (fecha.Minute < 10 ? "0" + fecha.Minute.ToString() : fecha.Minute.ToString());


                // Obtener las horas y minutos de la primera variable
                int hora1 = int.Parse(HoraAgendaUser.Split(':')[0]);
                int minuto1 = int.Parse(HoraAgendaUser.Split(':')[1]);

                // Obtener las horas y minutos de la segunda variable
                int hora2 = int.Parse(HoraActual.Split(':')[0]);
                int minuto2 = int.Parse(HoraActual.Split(':')[1]);

                var hourAccept = (hora2 > hora1 || (hora2 == hora1 && minuto2 >= minuto1)) ? true : false;
                res.SheduleByDayAndHourAvailable = (fecha.DayOfWeek.ToString() == "Sunday" && hourAccept ? true : false);




                res.Data = shedule_.ToList();
                res.IsSuccess = shedule_.Count > 0;

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListDayAvailableByWeek")]
        public async Task<ActionResult> GetListDayAvailableByWeek([FromBody] SheduleDto request)
        {
            ResultDTO<ListCbDTO> res = new ResultDTO<ListCbDTO>();
            try
            {
                int iiid_user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                SheduleDto _request = new SheduleDto
                {
                    iid_shedule_weekly = -1,
                    vname_shedule_weekly = "",
                    vdescription_shedule_weekly = "",
                    iid_week_shedule_weekly = request.iid_week_shedule_weekly,
                    iid_day_shedule_weekly = -1,
                    vhour_shedule_weekly = "",
                    itype_stream = -1,
                    iid_user = -1,
                    iid_comunity = request.iid_comunity,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 100000,
                };

                ResultDTO<SheduleDto> resShedule = new ResultDTO<SheduleDto>();

                resShedule = await this.iISheduleAplication.GetListShedule(_request);

                //OBTENEMOS LOS DIAS DE LA SEMANA
                TableDetailDto requestDayWeek = new TableDetailDto
                {
                    iid_table_detail = -1,
                    iid_table_headboard = 8,
                    iid_detail = -1,
                    vdescription = "",
                    vvalue_text_1 = "",
                    vvalue_text_2 = "",
                    vvalue_text_3 = "",
                    vvalue_integer_1 = -1,
                    vvalue_integer_2 = -1,
                    vvalue_integer_3 = -1,
                    vvalue_decimal_1 = -1,
                    vvalue_decimal_2 = -1,
                    vvalue_decimal_3 = -1,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 1000,
                };
                var resDayWeek = await this.iIAuxiliaryTablesAplication.GetListTableDetail(requestDayWeek);

                DateTime fecha = DateTime.Now.AddHours(-4);
                if (fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    int dayWeek = resDayWeek.Data.FindIndex(p => p.vvalue_text_3 == fecha.DayOfWeek.ToString()) + 1;
                    resDayWeek.Data = resDayWeek.Data.FindAll(p => p.iid_detail >= dayWeek);
                }

                List<TableDetailDto> daysWeek = new List<TableDetailDto>();

                foreach(TableDetailDto item in resDayWeek.Data)
                {
                    int inumberstream = resShedule.Data.FindAll(p => p.iid_day_shedule_weekly == item.iid_detail).Count;
                    if(inumberstream < 28)
                    {
                        daysWeek.Add(item);
                    }
                }

                List<ListCbDTO> _list = daysWeek.Select(p => new ListCbDTO
                {
                    id = p.iid_detail,
                    vvalue1 = p.vvalue_text_1,
                    vvalue2 = p.vvalue_text_2,
                    vvalue3 = p.vvalue_text_3,
                    ivalue1 = p.vvalue_integer_1,
                    ivalue2 = p.vvalue_integer_2,
                    ivalue3 = p.vvalue_integer_3,
                    dvalue1 = p.vvalue_decimal_1,
                    dvalue2 = p.vvalue_decimal_2,
                    dvalue3 = p.vvalue_decimal_3,
                }).ToList();

                res.Data = [.. _list];
                res.IsSuccess = daysWeek.Count > 0 ? true : false;


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListHourAvailableByDay")]
        public async Task<ActionResult> GetListHourAvailableByDay([FromBody] SheduleDto request)
        {
            ResultDTO<ListCbDTO> res = new ResultDTO<ListCbDTO>();
            try
            {
                int iiid_user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                UserDto requestUser = new UserDto
                {
                    iid_user = iiid_user,
                    vfirst_name = "",
                    vlast_name = "",
                    vcode = "",
                    vemail = "",
                    itype_document = -1,
                    inumber_document = -1,
                    vphone = "",
                    iid_department = -1,
                    iid_profile = -1,
                    vaddress = "",
                    iid_comunity = -1,
                    iid_time_zone = -1,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 1000,

                };


                SheduleDto _request = new SheduleDto
                {
                    iid_shedule_weekly = -1,
                    vname_shedule_weekly = "",
                    vdescription_shedule_weekly = "",
                    iid_week_shedule_weekly = request.iid_week_shedule_weekly,
                    iid_day_shedule_weekly = request.iid_day_shedule_weekly,
                    vhour_shedule_weekly = "",
                    itype_stream = -1,
                    iid_user = -1,
                    iid_comunity = request.iid_comunity,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 100000,
                };

                TableDetailDto requestTimeZone = new TableDetailDto
                {
                    iid_table_detail = -1,
                    iid_table_headboard = 7,
                    iid_detail = -1,
                    vdescription = "",
                    vvalue_text_1 = "",
                    vvalue_text_2 = "",
                    vvalue_text_3 = "",
                    vvalue_integer_1 = -1,
                    vvalue_integer_2 = -1,
                    vvalue_integer_3 = -1,
                    vvalue_decimal_1 = -1,
                    vvalue_decimal_2 = -1,
                    vvalue_decimal_3 = -1,
                    istate_record = 1,
                    iindex = 0,
                    ilimit = 1000,
                };


                var resShedule =  await this.iISheduleAplication.GetListShedule(_request);
                var resUser = await this.iIUsersAplication.GetListUsers(requestUser);           
                var resTimeZone = await this.iIAuxiliaryTablesAplication.GetListTableDetail(requestTimeZone);


                var itime_mex = resTimeZone.Data.Find(p => p.iid_detail == 1);
                var itime_user = resTimeZone.Data.Find(p => p.iid_detail == resUser.Data[0].iid_time_zone);
                List<ListCbDTO> getTimeCb = Util.GetTimeZone((int)itime_user.vvalue_integer_1, itime_user.vvalue_text_1, (int)itime_mex.vvalue_integer_1);

                List<ListCbDTO> _getTimeCb = new List<ListCbDTO>();
                foreach (ListCbDTO item in getTimeCb)
                {
                    var searchHourCount = resShedule.Data.FindAll(p => p.vhour_shedule_weekly == item.vvalue3).Count;

                    if (searchHourCount < 2)
                    {
                        _getTimeCb.Add(item);
                    }

                }

                //List<ListCbDTO> _list = daysWeek.Select(p => new ListCbDTO
                //{
                //    id = p.iid_detail,
                //    vvalue1 = p.vvalue_text_1,
                //    vvalue2 = p.vvalue_text_2,
                //    vvalue3 = p.vvalue_text_3,
                //    ivalue1 = p.vvalue_integer_1,
                //    ivalue2 = p.vvalue_integer_2,
                //    ivalue3 = p.vvalue_integer_3,
                //    dvalue1 = p.vvalue_decimal_1,
                //    dvalue2 = p.vvalue_decimal_2,
                //    dvalue3 = p.vvalue_decimal_3,
                //}).ToList();

                res.Data = _getTimeCb.ToList();
                res.IsSuccess = _getTimeCb.Count > 0 ? true : false;


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

    }
}
