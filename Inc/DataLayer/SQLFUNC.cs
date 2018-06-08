using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Inc.Models;
using System.Configuration;

using System.IO;
using System.Drawing;

namespace Inc
{
    public class SQLFUNC
    {
        private static string strCRMARConnectionString = ConfigurationManager.ConnectionStrings["ComplaintsContext"].ConnectionString;
        public static bool UpdateComplaint(Complaint cmp)
        {
            using (SqlConnection cnn = new SqlConnection(strCRMARConnectionString))
            {
                cnn.Open();
                string strQuery = "update INCIDENT_person set Last_Name =@param_Lname, First_Name=@param_fname,Middle_Name=@param_Mname," +
                    "DOB=@param_dob,Street=@param_Street,City=@param_City,State=@param_State,Zip=@param_Zip,Home_Number=@param_homenumber," +
                    "Mobile_Number=@param_mobilenumber,Other_Number=@param_othernumber,AKA=@param_aka,Gender_Id=@Gender_Id where Id=@ComplainantId";
                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                if (cmp.Complainant.AKA == null)
                {
                    cmd.Parameters.AddWithValue("@param_aka", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_aka", cmp.Complainant.AKA);
                }
                if (cmp.Complainant.Last_Name == null)
                {
                    cmd.Parameters.AddWithValue("@param_Lname", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Lname", cmp.Complainant.Last_Name);
                }
                if (cmp.Complainant.First_Name == null)
                {
                    cmd.Parameters.AddWithValue("@param_fname", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_fname", cmp.Complainant.First_Name);
                }
                if (cmp.Complainant.Middle_Name == null)
                {
                    cmd.Parameters.AddWithValue("@param_Mname", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Mname", cmp.Complainant.Middle_Name);
                }
                cmd.Parameters.AddWithValue("@param_dob", cmp.Complainant.DOB);
                if (cmp.Complainant.Street == null)
                {
                    cmd.Parameters.AddWithValue("@param_Street", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Street", cmp.Complainant.Street);
                }
                if (cmp.Complainant.State == null)
                {
                    cmd.Parameters.AddWithValue("@param_State", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_State", cmp.Complainant.State);
                }
                if (cmp.Complainant.City == null)
                {
                    cmd.Parameters.AddWithValue("@param_City", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_City", cmp.Complainant.City);
                }
                if (cmp.Complainant.Zip == null)
                {
                    cmd.Parameters.AddWithValue("@param_Zip", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Zip", cmp.Complainant.Zip);
                }
                if (cmp.Complainant.Home_Number == null)
                {
                    cmd.Parameters.AddWithValue("@param_homenumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_homenumber", cmp.Complainant.Home_Number);
                }
                if (cmp.Complainant.Mobile_Number == null)
                {
                    cmd.Parameters.AddWithValue("@param_mobilenumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_mobilenumber", cmp.Complainant.Mobile_Number);
                }
                if (cmp.Complainant.Other_Number == null)
                {
                    cmd.Parameters.AddWithValue("@param_othernumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_othernumber", cmp.Complainant.Other_Number);
                }
                if(cmp.Complainant.Gender.Id != 0)
                { cmd.Parameters.AddWithValue("@Gender_Id", cmp.Complainant.Gender.Id); }
                else
                {
                    cmd.Parameters.AddWithValue("@Gender_Id", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@ComplainantId", cmp.Complainant.Id);
                cmd.ExecuteNonQuery();
                strQuery = "update INCIDENT_complaints set report_date=@reportdate,Incident_Occurance_Date=@inc_occ_date,Narrative=@narrative, " +
                    "Disposition_Id=@dispositionid,Incident_Type_Id=@inc_type_id,Incident_Location_Id=@inc_loc_id, " +
                    "report_written_by_id=@rpt_ins_id,report_reviewed_by_id=@rpt_rev_id,be_id=@beid where Id=@cmpid";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.Parameters.AddWithValue("@reportdate", Convert.ToDateTime(cmp.Report_Date));
                cmd.Parameters.AddWithValue("@inc_occ_date", Convert.ToDateTime(cmp.Incident_Occurance_Date));
                if (cmp.Be_Id == null)
                { cmd.Parameters.AddWithValue("@beid", DBNull.Value); }
                else
                {
                    cmd.Parameters.AddWithValue("@beid", cmp.Be_Id);
                }
                if (cmp.Narrative == null)
                {
                    cmd.Parameters.AddWithValue("@narrative", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@narrative", cmp.Narrative);
                }

                cmd.Parameters.AddWithValue("@cmpId", cmp.Id);
                if(cmp.Disposition.Id != 0)
                { cmd.Parameters.AddWithValue("@dispositionid", cmp.Disposition.Id); }
                else { cmd.Parameters.AddWithValue("@dispositionid", DBNull.Value); }
                if(cmp.Incident_Type.Id != 0)
                { cmd.Parameters.AddWithValue("@inc_type_id", cmp.Incident_Type.Id); }
                else
                {
                    cmd.Parameters.AddWithValue("@inc_type_id", DBNull.Value);
                }
                if(cmp.Incident_Location.Id != 0)
                { cmd.Parameters.AddWithValue("@inc_loc_id", cmp.Incident_Location.Id); }
                else
                {
                    cmd.Parameters.AddWithValue("@inc_loc_id", DBNull.Value);
                }
                if(cmp.Report_Reviewed_By.Id != 0)
                { cmd.Parameters.AddWithValue("@rpt_rev_id", cmp.Report_Reviewed_By.Id); }
                else
                {
                    cmd.Parameters.AddWithValue("@rpt_rev_id", DBNull.Value);
                }
                if(cmp.Report_Written_By.Id != 0)
                { cmd.Parameters.AddWithValue("@rpt_ins_id", cmp.Report_Written_By.Id); }
                else { cmd.Parameters.AddWithValue("@rpt_ins_id", DBNull.Value); }
                cmd.ExecuteNonQuery();
                strQuery = "delete from incident_equipments where complaint_id=@cmpid";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.Parameters.AddWithValue("@cmpid", cmp.Id);
                cmd.ExecuteNonQuery();
                strQuery = "delete from incident_person_of_interest where complaint_id=@cmpid";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.Parameters.AddWithValue("@cmpid", cmp.Id);
                cmd.ExecuteNonQuery();
                if (cmp.Equipment != null)
                {
                    foreach (var item in cmp.Equipment)
                    {
                        strQuery = "insert into INCIDENT_equipments (Value,Occurance_Date,Description,Status_Id,Type_Id,Complaint_Id)" +
                        " Values(@Value,@Occ_Date,@Desc,@StatusId,@TypeId,@CmptId)";
                        cmd = new SqlCommand(strQuery, cnn);
                        if (item.Value.ToString() != null)
                        {
                            cmd.Parameters.AddWithValue("@Value", item.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Value", DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@Occ_Date", Convert.ToDateTime(item.Occurance_Date));
                        if (item.Description != null)
                        { cmd.Parameters.AddWithValue("@Desc", item.Description); }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Desc", DBNull.Value);
                        }
                        if (item.Status.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@StatusId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@StatusId", item.Status.Id);
                        }
                        if (item.Type.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@TypeId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TypeId", item.Type.Id);
                        }
                        cmd.Parameters.AddWithValue("@CmptId", cmp.Id);
                        cmd.ExecuteNonQuery();
                        strQuery = "delete from INCIDENT_Equipment_Photo where equipment_id=@eqid";
                        cmd = new SqlCommand(strQuery, cnn);
                        cmd.Parameters.AddWithValue("@eqid", item.Id);
                        cmd.ExecuteNonQuery();
                        if (item.Pictures != null)
                        {
                            foreach (var pic in item.Pictures)
                            {
                                strQuery = "insert into INCIDENT_Equipment_Photo (Equipment_Id,photo,Description) values(@Id,@Pic,@Desc)";
                                cmd = new SqlCommand(strQuery, cnn);
                                cmd.Parameters.AddWithValue("@Id", new SqlCommand("select max(id) from Incident_equipments", cnn).ExecuteScalar());
                                if (pic.Photo != null)
                                { cmd.Parameters.AddWithValue("@Pic", pic.Photo); }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Pic", DBNull.Value);
                                }
                                if (pic.Description != null)
                                { cmd.Parameters.AddWithValue("@Desc", pic.Description); }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Desc", DBNull.Value);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                if (cmp.Person_Of_Interest != null)
                {
                    foreach (var item in cmp.Person_Of_Interest)
                    {

                        strQuery = "INSERT INTO INCIDENT_PERSON" +
          "(LAST_NAME,FIRST_NAME,MIDDLE_NAME,DOB,STREET,CITY,ZIP,HOME_NUMBER,MOBILE_NUMBER,Gender_Id,AKA)" +
             " VALUES (@param_Lname,@param_fname,@param_Mname,@param_dob,@param_Street,@param_City,@param_Zip,@param_homenumber," +
                 "@param_mobilenumber,@Gender_Id,@param_aka)";

                        cmd = new SqlCommand(strQuery, cnn);
                        if (item.Person.AKA == null)
                        {
                            cmd.Parameters.AddWithValue("@param_aka", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_aka", item.Person.AKA);
                        }
                        if (item.Person.Last_Name == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Lname", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Lname", item.Person.Last_Name);
                        }
                        if (item.Person.First_Name == null)
                        {
                            cmd.Parameters.AddWithValue("@param_fname", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_fname", item.Person.First_Name);
                        }
                        if (item.Person.Middle_Name == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Mname", DBNull.Value);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Mname", item.Person.Middle_Name);
                        }

                        cmd.Parameters.AddWithValue("@param_dob", item.Person.DOB);
                        if (item.Person.Street == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Street", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Street", item.Person.Street);
                        }
                        if (item.Person.State == null)
                        {
                            cmd.Parameters.AddWithValue("@param_State", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_State", item.Person.State);
                        }
                        if (item.Person.City == null)
                        {
                            cmd.Parameters.AddWithValue("@param_City", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_City", item.Person.City);
                        }
                        if (item.Person.Zip == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Zip", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Zip", item.Person.Zip);
                        }
                        if (item.Person.Home_Number == null)
                        {
                            cmd.Parameters.AddWithValue("@param_homenumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_homenumber", item.Person.Home_Number);
                        }
                        if (item.Person.Mobile_Number == null)
                        {
                            cmd.Parameters.AddWithValue("@param_mobilenumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_mobilenumber", item.Person.Mobile_Number);
                        }
                        if (item.Person.Other_Number == null)
                        {
                            cmd.Parameters.AddWithValue("@param_othernumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_othernumber", item.Person.Other_Number);
                        }
                        if(item.Person.Gender.Id != 0)
                        { cmd.Parameters.AddWithValue("@Gender_Id", item.Person.Gender.Id); }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Gender_Id", DBNull.Value);
                        }
                        cmd.ExecuteNonQuery();
                        strQuery = "insert into INCIDENT_Person_Of_Interest (Comments,Person_Id,Type_Id,Complaint_Id)" +
                 " Values(@Comments,@PersonId,@Type_Id,@Complaint_Id)";
                        cmd = new SqlCommand(strQuery, cnn);
                        if (item.Comments != null)
                        {
                            cmd.Parameters.AddWithValue("@Comments", item.Comments);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Comments", DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@PersonId", new SqlCommand("select max(Id) from Incident_Person", cnn).ExecuteScalar());

                        if (item.Type.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@Type_Id", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Type_Id", item.Type.Id);
                        }
                        cmd.Parameters.AddWithValue("@Complaint_Id", cmp.Id);
                        cmd.ExecuteNonQuery();
                        strQuery = "delete from incident_poi_photo where poi_id=@poiId";
                        cmd = new SqlCommand(strQuery, cnn);
                        cmd.Parameters.AddWithValue("@poiId", item.Id);
                        cmd.ExecuteNonQuery();
                        if (item.Pictures != null)
                        {
                            foreach (var pic in item.Pictures)
                            {

                                strQuery = "insert into INCIDENT_POI_Photo (POI_Id,photo,Description) values(@Id,@Pic,@Desc)";
                                cmd = new SqlCommand(strQuery, cnn);
                                cmd.Parameters.AddWithValue("@Id", new SqlCommand("select max(id) from Incident_person_of_interest", cnn).ExecuteScalar());
                                if (pic.Photo != null)
                                {
                                    cmd.Parameters.AddWithValue("@Pic", pic.Photo);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Pic", DBNull.Value);
                                }
                                if (pic.Description != null)
                                {
                                    cmd.Parameters.AddWithValue("@Desc", pic.Description);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Desc", DBNull.Value);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            return true;
        }
        public static bool InsertNewComplaint(Complaint cmp)
        {
            using (SqlConnection cnn = new SqlConnection(strCRMARConnectionString))
            {
                cnn.Open();
                string strQuery = "INSERT INTO INCIDENT_PERSON" +
                 "(LAST_NAME,FIRST_NAME,MIDDLE_NAME,DOB,STREET,CITY,ZIP,HOME_NUMBER,MOBILE_NUMBER,Gender_Id,AKA)" +
                    " VALUES (@param_Lname,@param_fname,@param_Mname,@param_dob,@param_Street,@param_City,@param_Zip,@param_homenumber," +
                        "@param_mobilenumber,@Gender_Id,@param_aka)";

                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                if (cmp.Complainant.AKA == null)
                {
                    cmd.Parameters.AddWithValue("@param_aka", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_aka", cmp.Complainant.AKA);
                }
                if (cmp.Complainant.Last_Name == null)
                {
                    cmd.Parameters.AddWithValue("@param_Lname", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Lname", cmp.Complainant.Last_Name);
                }
                if (cmp.Complainant.First_Name == null)
                {
                    cmd.Parameters.AddWithValue("@param_fname", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_fname", cmp.Complainant.First_Name);
                }
                if (cmp.Complainant.Middle_Name == null)
                {
                    cmd.Parameters.AddWithValue("@param_Mname", DBNull.Value);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Mname", cmp.Complainant.Middle_Name);
                }

                cmd.Parameters.AddWithValue("@param_dob", cmp.Complainant.DOB);
                if (cmp.Complainant.Street == null)
                {
                    cmd.Parameters.AddWithValue("@param_Street", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Street", cmp.Complainant.Street);
                }
                if (cmp.Complainant.State == null)
                {
                    cmd.Parameters.AddWithValue("@param_State", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_State", cmp.Complainant.State);
                }
                if (cmp.Complainant.City == null)
                {
                    cmd.Parameters.AddWithValue("@param_City", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_City", cmp.Complainant.City);
                }
                if (cmp.Complainant.Zip == null)
                {
                    cmd.Parameters.AddWithValue("@param_Zip", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_Zip", cmp.Complainant.Zip);
                }
                if (cmp.Complainant.Home_Number == null)
                {
                    cmd.Parameters.AddWithValue("@param_homenumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_homenumber", cmp.Complainant.Home_Number);
                }
                if (cmp.Complainant.Mobile_Number == null)
                {
                    cmd.Parameters.AddWithValue("@param_mobilenumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_mobilenumber", cmp.Complainant.Mobile_Number);
                }
                if (cmp.Complainant.Other_Number == null)
                {
                    cmd.Parameters.AddWithValue("@param_othernumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param_othernumber", cmp.Complainant.Other_Number);
                }
                if(cmp.Complainant.Gender.Id != 0)
                { cmd.Parameters.AddWithValue("@Gender_Id", cmp.Complainant.Gender.Id); }
                else
                {
                    cmd.Parameters.AddWithValue("@Gender_Id", DBNull.Value);
                }
                cmd.ExecuteNonQuery();

                strQuery = "insert into INCIDENT_complaints(report_date,incident_occurance_date,narrative,Complainant_Id,Disposition_Id,Incident_Type_Id,Incident_Location_Id,Report_Reviewed_By_Id,Report_Written_By_Id,Be_Id)" +
                    "values(@reportdate,@inc_occ_date,@narrative,@ComplainantId,@dispositionid,@inc_type_id," +
                    "@inc_loc_id,@rpt_rev_id,@rpt_ins_id,@beid)";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.Parameters.AddWithValue("@reportdate", Convert.ToDateTime(cmp.Report_Date));
                cmd.Parameters.AddWithValue("@inc_occ_date", Convert.ToDateTime(cmp.Incident_Occurance_Date));
                if(cmp.Be_Id == null)
                { cmd.Parameters.AddWithValue("@beid", DBNull.Value); }
                else
                {
                    cmd.Parameters.AddWithValue("@beid", cmp.Be_Id);
                }
              
                if (cmp.Narrative == null)
                {
                    cmd.Parameters.AddWithValue("@narrative", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@narrative", cmp.Narrative);
                }

                cmd.Parameters.AddWithValue("@ComplainantId", new SqlCommand("select max(Id) from incident_person", cnn).ExecuteScalar());
                if(cmp.Disposition.Id != 0)
                {
                    cmd.Parameters.AddWithValue("@dispositionid", cmp.Disposition.Id);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@dispositionid", DBNull.Value);
                }
                if(cmp.Incident_Type.Id != 0)
                { cmd.Parameters.AddWithValue("@inc_type_id", cmp.Incident_Type.Id); }
                else { cmd.Parameters.AddWithValue("@inc_type_id", DBNull.Value); }
                if(cmp.Incident_Location.Id != 0)
                { cmd.Parameters.AddWithValue("@inc_loc_id", cmp.Incident_Location.Id); }
                else { cmd.Parameters.AddWithValue("@inc_loc_id", DBNull.Value); }
                if(cmp.Report_Reviewed_By.Id != 0)
                { cmd.Parameters.AddWithValue("@rpt_rev_id", cmp.Report_Reviewed_By.Id); }
                else { cmd.Parameters.AddWithValue("@rpt_rev_id", DBNull.Value); }
                if (cmp.Report_Written_By.Id != 0)
                { cmd.Parameters.AddWithValue("@rpt_ins_id", cmp.Report_Written_By.Id); }
                else { cmd.Parameters.AddWithValue("@rpt_ins_id", DBNull.Value); }                       
                cmd.ExecuteNonQuery();
                if (cmp.Equipment != null)
                {
                    foreach (var item in cmp.Equipment)
                    {
                        strQuery = "insert into INCIDENT_equipments (Value,Occurance_Date,Description,Status_Id,Type_Id,Complaint_Id)" +
                      " Values(@Value,@Occ_Date,@Desc,@StatusId,@TypeId,@CmptId)";
                        cmd = new SqlCommand(strQuery, cnn);
                        if (item.Value.ToString() != null)
                        {
                            cmd.Parameters.AddWithValue("@Value", item.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Value", DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@Occ_Date", Convert.ToDateTime(item.Occurance_Date));
                        if (item.Description != null)
                        {
                            cmd.Parameters.AddWithValue("@Desc", item.Description);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Desc", DBNull.Value);
                        }

                        if (item.Status.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@StatusId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@StatusId", item.Status.Id);
                        }
                        if (item.Type.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@TypeId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TypeId", item.Type.Id);
                        }
                        cmd.Parameters.AddWithValue("@CmptId", new SqlCommand("select max(Id) from Incident_Complaints", cnn).ExecuteScalar());
                        cmd.ExecuteNonQuery();
                        if (item.Pictures != null)
                        {
                            foreach (var pic in item.Pictures)
                            {
                                strQuery = "insert into INCIDENT_Equipment_Photo (Equipment_Id,photo,Description) values(@Id,@Pic,@Desc)";
                                cmd = new SqlCommand(strQuery, cnn);
                                cmd.Parameters.AddWithValue("@Id", new SqlCommand("select max(id) from Incident_equipments", cnn).ExecuteScalar());
                                if (pic.Photo != null)
                                { cmd.Parameters.AddWithValue("@Pic", pic.Photo); }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Pic", DBNull.Value);
                                }
                                if (pic.Description != null)
                                { cmd.Parameters.AddWithValue("@Desc", pic.Description); }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Desc", DBNull.Value);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                if (cmp.Person_Of_Interest != null)
                {
                    foreach (var item in cmp.Person_Of_Interest)
                    {
                        strQuery = "INSERT INTO INCIDENT_PERSON" +
               "(LAST_NAME,FIRST_NAME,MIDDLE_NAME,DOB,STREET,CITY,ZIP,HOME_NUMBER,MOBILE_NUMBER,Gender_Id,AKA)" +
                  " VALUES (@param_Lname,@param_fname,@param_Mname,@param_dob,@param_Street,@param_City,@param_Zip,@param_homenumber," +
                      "@param_mobilenumber,@Gender_Id,@param_aka)";

                        cmd = new SqlCommand(strQuery, cnn);
                        if (item.Person.AKA == null)
                        {
                            cmd.Parameters.AddWithValue("@param_aka", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_aka", item.Person.AKA);
                        }
                        if (item.Person.Last_Name == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Lname", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Lname", item.Person.Last_Name);
                        }
                        if (item.Person.First_Name == null)
                        {
                            cmd.Parameters.AddWithValue("@param_fname", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_fname", item.Person.First_Name);
                        }
                        if (item.Person.Middle_Name == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Mname", DBNull.Value);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Mname", item.Person.Middle_Name);
                        }

                        cmd.Parameters.AddWithValue("@param_dob", item.Person.DOB);
                        if (item.Person.Street == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Street", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Street", item.Person.Street);
                        }
                        if (item.Person.State == null)
                        {
                            cmd.Parameters.AddWithValue("@param_State", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_State", item.Person.State);
                        }
                        if (item.Person.City == null)
                        {
                            cmd.Parameters.AddWithValue("@param_City", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_City", item.Person.City);
                        }
                        if (item.Person.Zip == null)
                        {
                            cmd.Parameters.AddWithValue("@param_Zip", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_Zip", item.Person.Zip);
                        }
                        if (item.Person.Home_Number == null)
                        {
                            cmd.Parameters.AddWithValue("@param_homenumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_homenumber", item.Person.Home_Number);
                        }
                        if (item.Person.Mobile_Number == null)
                        {
                            cmd.Parameters.AddWithValue("@param_mobilenumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_mobilenumber", item.Person.Mobile_Number);
                        }
                        if (item.Person.Other_Number == null)
                        {
                            cmd.Parameters.AddWithValue("@param_othernumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@param_othernumber", item.Person.Other_Number);
                        }
                        if(item.Person.Gender.Id != 0)
                        { cmd.Parameters.AddWithValue("@Gender_Id", item.Person.Gender.Id); }
                        else { cmd.Parameters.AddWithValue("@Gender_Id", DBNull.Value); }
                       
                        cmd.ExecuteNonQuery();

                        strQuery = "insert into INCIDENT_Person_Of_Interest (Comments,Person_Id,Type_Id,Complaint_Id)" +
                      " Values(@Comments,@PersonId,@Type_Id,@Complaint_Id)";
                        cmd = new SqlCommand(strQuery, cnn);
                        if (item.Comments != null)
                        { cmd.Parameters.AddWithValue("@Comments", item.Comments); }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Comments", DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@PersonId", new SqlCommand("select max(Id) from Incident_Person", cnn).ExecuteScalar());
                        if (item.Type.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@Type_Id", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Type_Id", item.Type.Id);
                        }
                        cmd.Parameters.AddWithValue("@Complaint_Id", new SqlCommand("select max(Id) from Incident_Complaints", cnn).ExecuteScalar());
                        cmd.ExecuteNonQuery();
                        if (item.Pictures != null)
                        {
                            foreach (var pic in item.Pictures)
                            {
                                strQuery = "insert into INCIDENT_POI_Photo (POI_Id,photo,Description) values(@Id,@Pic,@Desc)";
                                cmd = new SqlCommand(strQuery, cnn);
                                cmd.Parameters.AddWithValue("@Id", new SqlCommand("select max(id) from Incident_person_of_interest", cnn).ExecuteScalar());
                                if (pic.Photo != null)
                                { cmd.Parameters.AddWithValue("@Pic", pic.Photo); }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Pic", DBNull.Value);
                                }
                                if (pic.Description != null)
                                { cmd.Parameters.AddWithValue("@Desc", pic.Description); }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Desc", DBNull.Value);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            return true;
        }
        public static Complaint GetComplaints(int ComplaintId)
        {
            int rptID=0,disID=0,rptrvID=0,incID=0,inclocId=0,genderId=0;
            Complaint complaint = null;
            SqlConnection conn = new SqlConnection(strCRMARConnectionString);
            conn.Open();
            string strQry = "select Person.Id AS [CompId],Person.Last_name,person.first_Name,person.Middle_Name," +
                "person.DOb,Person.Street,Person.City,Person.State,Person.Zip,Person.Home_Number,Person.Mobile_Number,Person.Other_Number," +
                "Person.AKA,Person.Gender_Id,Inc.Id As [Id],Inc.Report_Date,Inc.Incident_Occurance_Date,Inc.Narrative,Inc.Complainant_Id," +
                "Inc.Disposition_Id,Inc.Be_Id,Inc.Incident_Type_Id,Inc.Incident_Location_Id,Inc.Report_Reviewed_By_Id,Inc.Report_Written_By_Id,Incident_Type.Description,Incident_Location.Description AS [Desc] " +
                "from INCIDENT_Complaints Inc " +
                "inner join INCIDENT_Person Person on person.Id = Inc.Complainant_Id " +
                "left outer join INCIDENT_Type Incident_Type on Inc.Incident_Type_Id = Incident_Type.Id " +
                "left outer join Incident_Location on Inc.Incident_Location_Id = Incident_Location.Id " +
                "where Inc.Id = @p_Complaint_Id";

            SqlCommand cmd = new SqlCommand(strQry, conn);
            cmd.Parameters.AddWithValue("@p_Complaint_Id", ComplaintId);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int.TryParse(rdr["Report_Written_By_Id"].ToString(), out rptID);
                int.TryParse(rdr["Disposition_Id"].ToString(), out disID);
                int.TryParse(rdr["Report_Reviewed_By_Id"].ToString(), out rptrvID);
                int.TryParse(rdr["Incident_Type_Id"].ToString(), out incID);
                int.TryParse(rdr["Incident_Location_Id"].ToString(), out inclocId);
                int.TryParse(rdr["Gender_Id"].ToString(), out genderId);

                try
                {
                    complaint = new Complaint
                    {
                        Complainant = new Person
                        {
                            AKA = rdr["AKA"].ToString(),
                            City = rdr["City"].ToString(),
                            DOB = rdr["DOB"].ToString(),
                            First_Name = rdr["First_Name"].ToString(),
                            Last_Name = rdr["Last_Name"].ToString(),
                            Gender = new Gender() { Id = genderId },
                            Zip = rdr["zip"].ToString(),
                            Home_Number = rdr["home_number"].ToString(),
                            Id = int.Parse(rdr["CompId"].ToString()),
                            Street = rdr["street"].ToString(),
                            State = rdr["state"].ToString(),
                            Other_Number = rdr["other_number"].ToString(),
                            Middle_Name = rdr["middle_name"].ToString(),
                            Mobile_Number = rdr["mobile_number"].ToString()
                        },
                        Id = int.Parse(rdr["Id"].ToString()),
                        Be_Id = rdr["Be_Id"].ToString(),
                        Narrative = rdr["Narrative"].ToString(),
                        Report_Written_By = new Safety_Officer() { Id = rptID},
                        Disposition = new Disposition() { Id = disID },
                        Report_Reviewed_By = new Safety_Officer() { Id = rptrvID},
                        Report_Date = rdr["Report_Date"].ToString(),                       
                        Incident_Location = new Location() { Id = inclocId, Description = rdr["Desc"].ToString() },
                        Incident_Type = new Incident_Type() { Id = incID, Description = rdr["Description"].ToString() },
                        Incident_Occurance_Date = rdr["Incident_Occurance_Date"].ToString(),
                        Equipment = GetEquipments(rdr["Id"].ToString()),
                        Person_Of_Interest = GetPOI(int.Parse(rdr["Id"].ToString()))
                    };
                }
                catch (Exception e)
                {

                }
                finally
                {
                    //conn.Close();
                }

            }

            return complaint;
        }

        public static List<Complaint> GetMaingridComplaints()
        {
            List<Complaint> listComplaints = new List<Complaint>();
            SqlConnection conn = new SqlConnection(strCRMARConnectionString);
            conn.Open();
            string strQry = @"select Inc.Id As [Id], 
                            Incident_Type.Description,
                            Incident_Location.Description AS [Desc],
                            Inc.Report_Date,
                            Inc.Incident_Occurance_Date,
                            Person.Last_name,
                            person.first_Name
                            from INCIDENT_Complaints Inc
                            inner join INCIDENT_Person Person on person.Id = Inc.Complainant_Id
                            left outer join INCIDENT_Type Incident_Type on Inc.Incident_Type_Id = Incident_Type.Id
                            left outer join Incident_Location on Inc.Incident_Location_Id = Incident_Location.Id ";

            SqlCommand cmd = new SqlCommand(strQry, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                try
                {
                    listComplaints.Add(new Complaint
                    {
                        Complainant = new Person
                        {
                            First_Name = rdr["First_Name"].ToString(),
                            Last_Name = rdr["Last_Name"].ToString(),
                        },
                        Id = int.Parse(rdr["Id"].ToString()),
                        Report_Date = rdr["Report_Date"].ToString(),
                        Incident_Location = new Location() { Description = rdr["Desc"].ToString() },
                        Incident_Type = new Incident_Type() { Description = rdr["Description"].ToString() },
                        Incident_Occurance_Date = rdr["Incident_Occurance_Date"].ToString(),
                    });
                }
                catch (Exception e)
                {

                }
                finally
                {
                    //conn.Close();
                }

            }

            return listComplaints;
        }
        private static List<Person_Of_Interest> GetPOI(int Id)
        {
            List<Person_Of_Interest> lstPOI = new List<Person_Of_Interest>();
            try
            {
                int typeid = 0,genderid=0;
                string strQuery = "select p.Id,p.comments,p.person_id,p.type_id,ptype.Description,person.Id as [personId],person.First_Name,person.Middle_Name,person.Last_Name, " +
                   "person.dob,person.street,person.city,person.state,person.zip,person.home_number,person.mobile_number,person.other_number,person.Gender_Id,person.AKA " +
                   "from INCIDENT_Person_Of_Interest p" +
                   " left outer join Incident_POI_Type ptype on ptype.Id = p.Type_Id" +
               " left outer join INCIDENT_Person person on person.Id = p.Person_Id" +
                   " where p.Complaint_Id='" + Id + "'";
                SqlConnection cnn = new SqlConnection(strCRMARConnectionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        int.TryParse(rd["Type_Id"].ToString(), out typeid );
                        int.TryParse(rd["Gender_Id"].ToString(), out genderid);
                        lstPOI.Add(new Person_Of_Interest
                        {
                            Id = int.Parse(rd["Id"].ToString()),
                            Comments = rd["Comments"].ToString(),
                            Type = new POI_Type { Id = typeid, Description = rd["Description"].ToString() },

                            Person = new Person
                            {
                                AKA = rd["AKA"].ToString(),
                                City = rd["City"].ToString(),
                                DOB = rd["DOB"].ToString(),
                                First_Name = rd["First_Name"].ToString(),
                                Last_Name = rd["Last_Name"].ToString(),
                                Gender = new Gender() { Id = genderid },
                                Zip = rd["zip"].ToString(),
                                Home_Number = rd["home_number"].ToString(),
                                Id = int.Parse(rd["personId"].ToString()),
                                Street = rd["street"].ToString(),
                                State = rd["state"].ToString(),
                                Other_Number = rd["other_number"].ToString(),
                                Middle_Name = rd["middle_name"].ToString(),
                                Mobile_Number = rd["mobile_number"].ToString()
                            },
                            Pictures = GetPOIPictures(int.Parse(rd[0].ToString()))

                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lstPOI;
        }
        private static List<POI_Photo> GetPOIPictures(int Id)
        {
            List<POI_Photo> lstPhoto = new List<POI_Photo>();
            try
            {
                string strQuery = "select * from INCIDENT_POI_Photo" +
                " where POI_Id='" + Id + "'";
                SqlConnection cnn = new SqlConnection(strCRMARConnectionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        lstPhoto.Add(new POI_Photo
                        {
                            POI_Id = int.Parse(rd[0].ToString()),
                            Description = rd["Description"].ToString(),
                            Photo = rd["photo"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return lstPhoto;
        }
        private static List<Equipment> GetEquipments(string Id)
        {
            List<Equipment> lst = new List<Equipment>();
            try
            {
                int stid = 0, tpid = 0;
                string strQuery = "select eq.Id,eq.Value,eq.Occurance_Date,eq.Description,st.Id as [stId],st.Description as [stDesc], " +
                "tp.Id as [tpId],tp.Description as [tpDesc]" +
                "from INCIDENT_equipments eq" +
                " left outer join INCIDENT_Equipment_Status st on eq.status_Id = st.Id " +
                " left outer join INCIDENT_Equipment_Type tp on eq.type_Id = tp.Id " +
                " where eq.Complaint_Id='" + int.Parse(Id) + "'";
                SqlConnection cnn = new SqlConnection(strCRMARConnectionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        int.TryParse(rd["stId"].ToString(), out stid);
                        int.TryParse(rd["tpId"].ToString(), out tpid);
                        lst.Add(new Equipment
                        {
                            Id = int.Parse(rd[0].ToString()),
                            Value = float.Parse(rd["Value"].ToString()),
                            Occurance_Date = rd["Occurance_Date"].ToString(),
                            Description = rd["Description"].ToString(),
                            Status = new Equipment_Status { Id = stid, Description = rd["stDesc"].ToString() },
                            Type = new Equipment_Type { Id = tpid, Description = rd["tpDesc"].ToString() },
                            Pictures = GetEquipmentPictures(int.Parse(rd[0].ToString()))
                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return lst;
        }
        private static List<Equipment_Photo> GetEquipmentPictures(int Id)
        {
            List<Equipment_Photo> lstPhoto = new List<Equipment_Photo>();
            try
            {
                string strQuery = "select * from INCIDENT_Equipment_Photo" +
                " where Equipment_Id='" + Id + "'";
                SqlConnection cnn = new SqlConnection(strCRMARConnectionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        lstPhoto.Add(new Equipment_Photo
                        {
                            Equipment_Id = int.Parse(rd[0].ToString()),
                            Description = rd["Description"].ToString(),
                            Photo = rd["photo"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstPhoto;
        }
        public static List<ViewModel_DropDownGrid> GetTableValues(string TableName)
        {
            List<ViewModel_DropDownGrid> list_ViewModel_DropDownGrid = new List<ViewModel_DropDownGrid>();
            using (SqlConnection connection = new SqlConnection(strCRMARConnectionString))
            {
                connection.Open();

                string strQuery =
               string.Format(@"SELECT Id, Description,Active
                     FROM {0} ", TableName);

                SqlCommand cmd = new SqlCommand(strQuery, connection);
                SqlDataReader reader = null;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewModel_DropDownGrid viewModel = new ViewModel_DropDownGrid()
                    {

                        TableId = Convert.ToInt32(reader["ID"]),
                        Description = reader["DESCRIPTION"].ToString(),
                        Active = Convert.ToBoolean(reader["Active"])
                    };

                    list_ViewModel_DropDownGrid.Add(viewModel);
                }
            }

            return list_ViewModel_DropDownGrid;
        }
        public static void InsertTableValues(string TableName, ViewModel_DropDownGrid Griddata)
        {
            List<ViewModel_DropDownGrid> list_ViewModel_DropDownGrid = new List<ViewModel_DropDownGrid>();
            using (SqlConnection connection = new SqlConnection(strCRMARConnectionString))
            {
                connection.Open();

                string strQuery =
                    string.Format(@"INSERT INTO {0} (DESCRIPTION,ACTIVE) VALUES(@param_Description,@param_Active)", TableName);

                SqlCommand cmd = new SqlCommand(strQuery, connection);
                cmd.Parameters.AddWithValue("@param_Description", Griddata.Description);
                cmd.Parameters.AddWithValue("@param_Active", Griddata.Active);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        public static void UpdateTableValues(string TableName, ViewModel_DropDownGrid Griddata)
        {
            List<ViewModel_DropDownGrid> list_ViewModel_DropDownGrid = new List<ViewModel_DropDownGrid>();
            using (SqlConnection connection = new SqlConnection(strCRMARConnectionString))
            {
                connection.Open();

                string strQuery =
                    string.Format(@"UPDATE {0} SET DESCRIPTION =@param_Description,ACTIVE=@param_Active where id=@param_TableId", TableName);

                SqlCommand cmd = new SqlCommand(strQuery, connection);
                cmd.Parameters.AddWithValue("@param_Description", Griddata.Description);
                cmd.Parameters.AddWithValue("@param_Active", Griddata.Active);
                cmd.Parameters.AddWithValue("@param_TableId", Griddata.TableId);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        

        public static  DataTable GetIncidentReport(int  CmpId)
        {
            SqlConnection cnn = new SqlConnection(strCRMARConnectionString);
            
                cnn.Open();
                string strQuery = "select cmp.be_id, cmp.id,format(cmp.report_date,'mm/dd/yyyy hh:mm','en-us') as [report_date],format(cmp.incident_occurance_date,'mm/dd/yy hh:mm','en-us') as [incident_occurance_date],cmp.narrative,disp.description as [DispDesc], " +
                    "incloc.description as [LocDesc],inctype.description as [TypeDesc],writby.description as [WritDesc],revby.description as [RevDesc], " +
                    "p.gender_id,p.last_name,p.first_name,p.middle_name,p.dob,p.street,p.city,p.state,p.zip,p.home_number,p.mobile_number,p.other_number, " +
                    "incgender.description as [GendDesc],eq.Id,eq.complaint_id, eq.value,eq.occurance_date,eq.description as  [EquipDesc],eqst.Description as [StatusDesc]," +
                   "eqtp.description as [EqTypeDesc],eqph.equipment_id,eqph.photo,eqph.Description as [PhDesc] "+                 
                   "from incident_complaints cmp inner join incident_person p on cmp.complainant_id = p.id " +
                   "left outer join incident_equipments eq on eq.complaint_id = cmp.id " +
                   "left outer join incident_equipment_type eqtp on eq.type_id = eqtp.id " +
                   " left outer join incident_equipment_status eqst on eq.status_id = eqst.id "+               
                   " left outer join  incident_equipment_photo eqph on eq.id = eqph.equipment_id "+ 
                    "left outer join incident_disposition disp on cmp.disposition_id = disp.id " +
                    "left outer join incident_gender incgender on p.gender_id = incgender.id " +
                    "left outer join incident_location incloc on cmp.incident_location_id = incloc.id " +
                      "left outer join incident_type inctype on cmp.incident_type_id = inctype.id " +
                    "left outer join incident_report_reviewed_by revby on cmp.report_reviewed_by_id = revby.id  " +
                    "left outer join incident_report_written_by writby on cmp.report_written_by_id = writby.id  " +
                    "where cmp.id = " + CmpId + "";
                SqlCommand cmd = new SqlCommand(strQuery, cnn);
                DataTable Reportdt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter(cmd);         
              da.Fill(Reportdt);

            foreach (DataRow dr in Reportdt.Rows)
            {
                byte[] imageBytes = Convert.FromBase64String(dr["Photo"].ToString().Remove(0, 23));
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms, true);
                dr["Photo"] = imageBytes;
            }

            return Reportdt;         

      }

    }
}