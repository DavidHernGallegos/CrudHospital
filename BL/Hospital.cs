using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Hospital
    {
        public static Dictionary<string,object> GetAll()
        {
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string,object> diccionario = new Dictionary<string, object>{ {"Hospital", hospital },{"Resultado", false }, {"Mensaje", "" } };
            try
            {
                using(DL.CrudHospitalContext context = new DL.CrudHospitalContext())
                {
                    var query = (from Hospital in context.Hospitals
                                 join Especialidades in context.Especialidads on Hospital.IdEspecialidad equals Especialidades.IdEspecialidad
                                 select new { 
                                     IdHospital = Hospital.IdHospital,
                                     NombreHospital = Hospital.Nombre,
                                     Direccion = Hospital.Direccion,
                                     AñoConstruccion = Hospital.AñoCostruccion,
                                     Capacidad = Hospital.Capacidad,
                                     IdEspecialidad = Especialidades.IdEspecialidad
                                
                                 }).ToList();

                    hospital.Hospitales = new List<object>();

                    if(query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Hospital objHospital = new ML.Hospital();
                            objHospital.IdHospital = item.IdHospital;
                            objHospital.Nombre = item.NombreHospital;
                            objHospital.Direccion = item.Direccion;
                            objHospital.AñoCostruccion = item.AñoConstruccion;
                            objHospital.Capacidad = item.Capacidad;
                            objHospital.Especialidad = new ML.Especialidad();
                            objHospital.Especialidad.IdEspecialidad = item.IdEspecialidad;
                            hospital.Hospitales.Add(objHospital);
                        }

                        diccionario["Hospital"] = hospital;
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han recuperado los datos";
                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se han recuperado los datos";
                    }
                }
  

            }
            catch(Exception EX) 
            {
                diccionario["Mensaje"] = "No se han recuperado los datos" + EX;
            }

            return diccionario;
        }


        public static Dictionary<string, object> GetById(int IdHospital)
        {
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Hospital", hospital }, { "Resultado", false }, { "Mensaje", "" } };
            try
            {
                using (DL.CrudHospitalContext context = new DL.CrudHospitalContext())
                {
                    var query = (from Hospital in context.Hospitals
                                 join Especialidades in context.Especialidads on Hospital.IdEspecialidad equals Especialidades.IdEspecialidad
                                 where Hospital.IdHospital == IdHospital
                                 select new
                                 {
                                     IdHospital = Hospital.IdHospital,
                                     NombreHospital = Hospital.Nombre,
                                     Direccion = Hospital.Direccion,
                                     AñoConstruccion = Hospital.AñoCostruccion,
                                     Capacidad = Hospital.Capacidad,
                                     IdEspecialidad = Especialidades.IdEspecialidad

                                 }).SingleOrDefault();

                    if (query != null)
                    {
                       
                       
                            hospital.IdHospital = query.IdHospital;
                            hospital.Nombre = query.NombreHospital;
                            hospital.Direccion = query.Direccion;
                            hospital.AñoCostruccion = query.AñoConstruccion;
                            hospital.Capacidad = query.Capacidad;
                            hospital.Especialidad = new ML.Especialidad();
                            hospital.Especialidad.IdEspecialidad = query.IdEspecialidad;
                         
                        diccionario["Hospital"] = hospital;
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han recuperado los datos";
                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se han recuperado los datos";
                    }
                }


            }
            catch (Exception EX)
            {
                diccionario["Mensaje"] = "No se han recuperado los datos" + EX;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Delete(int IdHospital)
        {
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };
            try
            {
                using (DL.CrudHospitalContext context = new DL.CrudHospitalContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteHospital {IdHospital}");

                    if (query > 0)
                    {

                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han eliminado los datos";
                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se han eliminado los datos";
                    }
                }


            }
            catch (Exception EX)
            {
                diccionario["Mensaje"] = "No se han eliminado los datos" + EX;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Add(ML.Hospital hospital)
        {
   
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };
            try
            {
                using (DL.CrudHospitalContext context = new DL.CrudHospitalContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ADDHospital '{hospital.Nombre}','{hospital.Direccion}', '{hospital.AñoCostruccion}', {hospital.Capacidad}, {hospital.Especialidad.IdEspecialidad} ");

                    if (query > 0)
                    {

                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han Agregado los datos";
                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se han agregado los datos";
                    }
                }


            }
            catch (Exception EX)
            {
                diccionario["Mensaje"] = "No se han agregado los datos" + EX;
            }

            return diccionario;
        }



    }
}
