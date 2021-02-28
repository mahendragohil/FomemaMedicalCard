using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FomemaMedicalCard.Models;
using System.Globalization;

namespace FomemaMedicalCard.DataLayer
{
    public class FomemaDBUtility
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["FomemaDBConnection"].ConnectionString;
        public const string CadidatePhotoFolderName = "UploadedCandidateProfile";
        public const string ImageUploadRootPath = "/Content/Images/";
        public static DataTable ValidateUserLogin(LoginModel login)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("ValidateFomemaUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = login.UserId;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = login.Password;
            conn.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            return dt;

        }

        public static CandidateModel GetCandidateMedicalCardDetails(string CandidateCardNumber)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("CandidateMedicalCardGetData", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CandidateCardNumber", CandidateCardNumber);
            conn.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            CandidateModel candidateModel = new CandidateModel();
            if (dt.Rows.Count == 1)
            {
                string StrCovid19TestDate1 = string.Empty;
                string StrCovid19TestDate2 = string.Empty;
                string StrCovid19TestDate3 = string.Empty;
                string StrCovid19TestDate4 = string.Empty;
                string StrVaccineDose1Date = string.Empty;
                string StrVaccineDose2Date = string.Empty;

                if (dt.Rows[0]["Covid19TestDate1"] != DBNull.Value)
                {
                    DateTime Covid19TestDate1 = Convert.ToDateTime(dt.Rows[0]["Covid19TestDate1"]);
                    StrCovid19TestDate1 = Covid19TestDate1.ToString("dd-MM-yyyy");
                }
                if (dt.Rows[0]["Covid19TestDate2"] != DBNull.Value)
                {
                    DateTime Covid19TestDate2 = Convert.ToDateTime(dt.Rows[0]["Covid19TestDate1"]);
                    StrCovid19TestDate2 = Covid19TestDate2.ToString("dd-MM-yyyy");
                }
                if (dt.Rows[0]["Covid19TestDate3"] != DBNull.Value)
                {
                    DateTime Covid19TestDate3 = Convert.ToDateTime(dt.Rows[0]["Covid19TestDate1"]);
                    StrCovid19TestDate3 = Covid19TestDate3.ToString("dd-MM-yyyy");
                }
                if (dt.Rows[0]["Covid19TestDate4"] != DBNull.Value)
                {
                    DateTime Covid19TestDate4 = Convert.ToDateTime(dt.Rows[0]["Covid19TestDate1"]);
                    StrCovid19TestDate4 = Covid19TestDate4.ToString("dd-MM-yyyy");
                }
                if (dt.Rows[0]["VaccineDose1Date"] != DBNull.Value)
                {
                    DateTime VaccineDose1Date = Convert.ToDateTime(dt.Rows[0]["Covid19TestDate1"]);
                    StrVaccineDose1Date = VaccineDose1Date.ToString("dd-MM-yyyy");
                }
                if (dt.Rows[0]["VaccineDose2Date"] != DBNull.Value)
                {
                    DateTime VaccineDose2Date = Convert.ToDateTime(dt.Rows[0]["Covid19TestDate1"]);
                    StrVaccineDose2Date = VaccineDose2Date.ToString("dd-MM-yyyy");
                }

                candidateModel.PictureURL = dt.Rows[0]["PictureURL"].ToString();
                candidateModel.Name = dt.Rows[0]["Name"].ToString();
                if (Convert.ToString(dt.Rows[0]["Age"]) == "")
                {
                    candidateModel.Age = null;
                }
                else
                {
                    candidateModel.Age = Convert.ToInt32(dt.Rows[0]["Age"].ToString());
                }
                string CardNumber = Convert.ToString(dt.Rows[0]["CandidateCardNumber"].ToString());
                candidateModel.CandidateCardNumber = string.Format("{0}-{1}-{2}", CardNumber.Substring(0, 3),
                          CardNumber.Substring(3, 3), CardNumber.Substring(6));
                candidateModel.NewPassportNo = dt.Rows[0]["NewPassportNo"].ToString();
                candidateModel.OldPassportNo = dt.Rows[0]["OldPassportNo"].ToString();
                candidateModel.CountryName = dt.Rows[0]["CountryName"].ToString();
                candidateModel.Covid19TestDate1 = StrCovid19TestDate1;
                candidateModel.Covid19TestDate2 = StrCovid19TestDate2;
                candidateModel.Covid19TestDate3 = StrCovid19TestDate3;
                candidateModel.Covid19TestDate4 = StrCovid19TestDate4;
                candidateModel.VaccineDose1Date = StrVaccineDose1Date;
                candidateModel.VaccineDose2Date = StrVaccineDose2Date;
                candidateModel.ClinicName = dt.Rows[0]["ClinicName"].ToString();
                candidateModel.ClinicLocation = dt.Rows[0]["ClinicLocation"].ToString();
            }

            return candidateModel;

        }

        public static DataTable GetCardListRecord(string CandidateCardNumber)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("sp_GetCardListRecord", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CardNumber", CandidateCardNumber);
            conn.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            return dt;
        }

        public static DataTable GetCardMasterRecord(string CandidateCardNumber)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("sp_GetCardMasterRecord", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CardNumber", CandidateCardNumber);
            conn.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            return dt;
        }

        //public static List<CandidateModel> GetAllCandidate()
        //{
        //    SqlConnection conn = new SqlConnection(connStr);

        //    SqlCommand cmd = new SqlCommand("CandidateMedicalCardGetData", conn);
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@CandidateId", SqlDbType.Int).Value = 0;
        //    conn.Open();

        //    DataTable dt = new DataTable();
        //    dt.Load(cmd.ExecuteReader());
        //    conn.Close();

        //    List<CandidateModel> candidateListModel = new List<CandidateModel>();

        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string StrCovid19TestDate1 = string.Empty;
        //            string StrCovid19TestDate2 = string.Empty;
        //            string StrCovid19TestDate3 = string.Empty;
        //            string StrCovid19TestDate4 = string.Empty;
        //            string StrVaccineDose1Date = string.Empty;
        //            string StrVaccineDose2Date = string.Empty;
        //            string StrDateofBirth = string.Empty;

        //            #region Date Formatting

        //            if (dt.Rows[i]["DateofBirth"] != DBNull.Value)
        //            {
        //                DateTime DateofBirth = Convert.ToDateTime(dt.Rows[i]["DateofBirth"]);
        //                StrDateofBirth = DateofBirth.ToString("dd-MM-yyyy");
        //            }
        //            if (dt.Rows[i]["Covid19TestDate1"] != DBNull.Value)
        //            {
        //                DateTime Covid19TestDate1 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
        //                StrCovid19TestDate1 = Covid19TestDate1.ToString("dd-MM-yyyy");
        //            }
        //            if (dt.Rows[i]["Covid19TestDate2"] != DBNull.Value)
        //            {
        //                DateTime Covid19TestDate2 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
        //                StrCovid19TestDate2 = Covid19TestDate2.ToString("dd-MM-yyyy");
        //            }
        //            if (dt.Rows[i]["Covid19TestDate3"] != DBNull.Value)
        //            {
        //                DateTime Covid19TestDate3 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
        //                StrCovid19TestDate3 = Covid19TestDate3.ToString("dd-MM-yyyy");
        //            }
        //            if (dt.Rows[i]["Covid19TestDate4"] != DBNull.Value)
        //            {
        //                DateTime Covid19TestDate4 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
        //                StrCovid19TestDate4 = Covid19TestDate4.ToString("dd-MM-yyyy");
        //            }
        //            if (dt.Rows[i]["VaccineDose1Date"] != DBNull.Value)
        //            {
        //                DateTime VaccineDose1Date = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
        //                StrVaccineDose1Date = VaccineDose1Date.ToString("dd-MM-yyyy");
        //            }
        //            if (dt.Rows[i]["VaccineDose2Date"] != DBNull.Value)
        //            {
        //                DateTime VaccineDose2Date = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
        //                StrVaccineDose2Date = VaccineDose2Date.ToString("dd-MM-yyyy");
        //            }

        //            #endregion

        //            CandidateModel candidateModel = new CandidateModel();
        //            candidateModel.Name = dt.Rows[i]["Name"].ToString();
        //            candidateModel.DateofBirth = StrDateofBirth;
        //            candidateModel.Age = dt.Rows[i]["Age"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["Age"].ToString());
        //            candidateModel.NewPassportNo = dt.Rows[i]["NewPassportNo"].ToString();
        //            candidateModel.OldPassportNo = dt.Rows[i]["OldPassportNo"].ToString();
        //            candidateModel.CountryName = dt.Rows[i]["CountryName"].ToString();
        //            candidateModel.FomemaTestYear = dt.Rows[i]["FomemaTestYear"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["FomemaTestYear"].ToString());
        //            candidateModel.FomemaTestResult = dt.Rows[i]["FomemaTestResult"].ToString();
        //            candidateModel.CountryName = dt.Rows[i]["CountryName"].ToString();
        //            candidateModel.Covid19TestDate1 = StrCovid19TestDate1;
        //            candidateModel.Covid19TestDate2 = StrCovid19TestDate2;
        //            candidateModel.Covid19TestDate3 = StrCovid19TestDate3;
        //            candidateModel.Covid19TestDate4 = StrCovid19TestDate4;
        //            candidateModel.VaccineDose1Date = StrVaccineDose1Date;
        //            candidateModel.VaccineDose2Date = StrVaccineDose2Date;
        //            candidateModel.ClinicName = dt.Rows[i]["ClinicName"].ToString();
        //            candidateModel.ClinicLocation = dt.Rows[i]["ClinicLocation"].ToString();
        //            candidateListModel.Add(candidateModel);
        //        }
        //    }

        //    return candidateListModel;
        //}

        public static DataTable CheckICNumberAvailibility(string DoctorsICNumber)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("ValidateDoctorsICNumber", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@DoctorsICNumber", SqlDbType.VarChar).Value = DoctorsICNumber;
            conn.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            return dt;
        }

        public static Tuple<List<CandidateModel>, int> GetAllCandidateForGrid(int PageIndex, int PageSize, string SortField, string SortOrder, string CandidateCardNumber, string Name, int Age, string NewPassportNo, string OldPassportNo,
            string CountryName, int FomemaTestYear, string FomemaTestResult, int UserType, string SearchPassportNo)
        {
            int totalCount = 0;
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("GetCandidateDetails", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = PageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
            cmd.Parameters.Add("@SortCol", SqlDbType.NVarChar).Value = SortField;
            cmd.Parameters.Add("@SortDir", SqlDbType.NVarChar).Value = SortOrder;
            cmd.Parameters.Add("@CandidateCardNumber", SqlDbType.VarChar).Value = CandidateCardNumber;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            cmd.Parameters.Add("@NewPassportNo", SqlDbType.NVarChar).Value = NewPassportNo;
            cmd.Parameters.Add("@OldPassportNo", SqlDbType.NVarChar).Value = OldPassportNo;
            cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = CountryName;
            cmd.Parameters.Add("@FomemaTestYear", SqlDbType.Int).Value = FomemaTestYear;
            cmd.Parameters.Add("@FomemaTestResult", SqlDbType.NVarChar).Value = FomemaTestResult;
            cmd.Parameters.Add("@UserType", SqlDbType.Int).Value = UserType;
            cmd.Parameters.Add("@SearchPassportNo", SqlDbType.NVarChar).Value = SearchPassportNo;
            conn.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            List<CandidateModel> candidateListModel = new List<CandidateModel>();

            if (dt.Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dt.Rows[0]["TotalCount"]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string StrCovid19TestDate1 = string.Empty;
                    string StrCovid19TestDate2 = string.Empty;
                    string StrCovid19TestDate3 = string.Empty;
                    string StrCovid19TestDate4 = string.Empty;
                    string StrVaccineDose1Date = string.Empty;
                    string StrVaccineDose2Date = string.Empty;
                    string StrDateofBirth = string.Empty;

                    #region Date Formatting

                    if (dt.Rows[i]["DateofBirth"] != DBNull.Value)
                    {
                        DateTime DateofBirth = Convert.ToDateTime(dt.Rows[i]["DateofBirth"]);
                        StrDateofBirth = DateofBirth.ToString("dd-MM-yyyy");
                    }
                    if (dt.Rows[i]["Covid19TestDate1"] != DBNull.Value)
                    {
                        DateTime Covid19TestDate1 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
                        StrCovid19TestDate1 = Covid19TestDate1.ToString("dd-MM-yyyy");
                    }
                    if (dt.Rows[i]["Covid19TestDate2"] != DBNull.Value)
                    {
                        DateTime Covid19TestDate2 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
                        StrCovid19TestDate2 = Covid19TestDate2.ToString("dd-MM-yyyy");
                    }
                    if (dt.Rows[i]["Covid19TestDate3"] != DBNull.Value)
                    {
                        DateTime Covid19TestDate3 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
                        StrCovid19TestDate3 = Covid19TestDate3.ToString("dd-MM-yyyy");
                    }
                    if (dt.Rows[i]["Covid19TestDate4"] != DBNull.Value)
                    {
                        DateTime Covid19TestDate4 = Convert.ToDateTime(dt.Rows[i]["Covid19TestDate1"]);
                        StrCovid19TestDate4 = Covid19TestDate4.ToString("dd-MM-yyyy");
                    }
                    if (dt.Rows[i]["VaccineDose1Date"] != DBNull.Value)
                    {
                        DateTime VaccineDose1Date = Convert.ToDateTime(dt.Rows[i]["VaccineDose1Date"]);
                        StrVaccineDose1Date = VaccineDose1Date.ToString("dd-MM-yyyy");
                    }
                    if (dt.Rows[i]["VaccineDose2Date"] != DBNull.Value)
                    {
                        DateTime VaccineDose2Date = Convert.ToDateTime(dt.Rows[i]["VaccineDose2Date"]);
                        StrVaccineDose2Date = VaccineDose2Date.ToString("dd-MM-yyyy");
                    }

                    #endregion

                    CandidateModel candidateModel = new CandidateModel();
                    candidateModel.CandidateId = Convert.ToInt32(dt.Rows[i]["CandidateId"].ToString());
                    string CardNumber = Convert.ToString(dt.Rows[i]["CandidateCardNumber"].ToString());
                    candidateModel.CandidateCardNumber = string.Format("{0}-{1}-{2}", CardNumber.Substring(0, 3),
                              CardNumber.Substring(3, 3), CardNumber.Substring(6));
                    candidateModel.CandidateGuid = new Guid(dt.Rows[i]["CandidateGuid"].ToString());
                    candidateModel.PictureURL = string.IsNullOrWhiteSpace(dt.Rows[i]["PictureURL"].ToString()) ? null : dt.Rows[i]["PictureURL"].ToString();
                    candidateModel.Name = dt.Rows[i]["Name"].ToString();
                    candidateModel.DateofBirth = StrDateofBirth;
                    if (Convert.ToString(dt.Rows[i]["Age"]) == "")
                    {
                        candidateModel.Age = null;
                    }
                    else
                    {
                        candidateModel.Age = Convert.ToInt32(dt.Rows[i]["Age"].ToString());
                    }
                    candidateModel.NewPassportNo = dt.Rows[i]["NewPassportNo"].ToString();
                    candidateModel.OldPassportNo = dt.Rows[i]["OldPassportNo"].ToString();
                    candidateModel.CountryName = dt.Rows[i]["CountryName"].ToString();
                    if (Convert.ToString(dt.Rows[i]["FomemaTestYear"]) == "")
                    {
                        candidateModel.FomemaTestYear = null;
                    }
                    else
                    {
                        candidateModel.FomemaTestYear = Convert.ToInt32(dt.Rows[i]["FomemaTestYear"].ToString());
                    }
                    candidateModel.FomemaTestResult = dt.Rows[i]["FomemaTestResult"].ToString();
                    candidateModel.CountryName = dt.Rows[i]["CountryName"].ToString();
                    candidateModel.Covid19TestDate1 = StrCovid19TestDate1;
                    candidateModel.Covid19TestDate2 = StrCovid19TestDate2;
                    candidateModel.Covid19TestDate3 = StrCovid19TestDate3;
                    candidateModel.Covid19TestDate4 = StrCovid19TestDate4;
                    candidateModel.VaccineDose1Date = StrVaccineDose1Date;
                    candidateModel.VaccineDose2Date = StrVaccineDose2Date;
                    candidateModel.ClinicName = dt.Rows[i]["ClinicName"].ToString();
                    candidateModel.ClinicLocation = dt.Rows[i]["ClinicLocation"].ToString();
                    candidateListModel.Add(candidateModel);
                }
            }

            return new Tuple<List<CandidateModel>, int>(candidateListModel, totalCount);
        }

        public static bool ClinicRegistrationSave(ClinicRegistrationModel clinicRegistration)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("ClinicRegitrationSave", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClinicName", SqlDbType.VarChar).Value = clinicRegistration.ClinicName;
            cmd.Parameters.Add("@DoctorName", SqlDbType.VarChar).Value = clinicRegistration.DoctorName;
            cmd.Parameters.Add("@DoctorsICNumber", SqlDbType.VarChar).Value = clinicRegistration.DoctorsICNumber;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = clinicRegistration.Password;
            cmd.Parameters.Add("@UserType", SqlDbType.Int).Value = CommonEnums.UserType.Clinic.GetHashCode();
            conn.Open();

            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            conn.Close();

            return result;

        }

        #region KIRAN CODE
        public static int IUDCandidateDetails(CandidateModel candidateModel, DataOperationMode dataOperationMode)
        {
            int result = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("IUDSaveCandidateDetails", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DataOperationMode", (int)dataOperationMode);
                cmd.Parameters.AddWithValue("@CandidateId", candidateModel.CandidateId);
                if (dataOperationMode == DataOperationMode.Update || dataOperationMode == DataOperationMode.Delete)
                {
                    cmd.Parameters.AddWithValue("@ModifiedBy", 1);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.UtcNow);
                }
                if (dataOperationMode == DataOperationMode.Insert)
                {
                    cmd.Parameters.AddWithValue("@CreatedBy", 1);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                }
                if (dataOperationMode != DataOperationMode.Delete)
                {
                    cmd.Parameters.AddWithValue("@CandidateCardNumber", candidateModel.CandidateCardNumber);
                    cmd.Parameters.AddWithValue("@CandidateCardNumberEncrypt", candidateModel.CandidateCardNumberEncrypted);
                    cmd.Parameters.AddWithValue("@CandidateGuid", candidateModel.CandidateGuid);
                    cmd.Parameters.AddWithValue("@Name", candidateModel.Name);
                    cmd.Parameters.AddWithValue("@DateofBirth", GetFormattedDateTime(candidateModel.DateofBirth));
                    cmd.Parameters.AddWithValue("@Age", candidateModel.Age);
                    cmd.Parameters.AddWithValue("@NewPassportNo", candidateModel.NewPassportNo);
                    cmd.Parameters.AddWithValue("@OldPassportNo", candidateModel.OldPassportNo);
                    cmd.Parameters.AddWithValue("@PictureURL", candidateModel.PictureURL);
                    cmd.Parameters.AddWithValue("@CountryName", candidateModel.CountryName);
                    cmd.Parameters.AddWithValue("@IsDeleted", dataOperationMode == DataOperationMode.Delete);
                    cmd.Parameters.AddWithValue("@RedirectedPath", candidateModel.RedirectedPath);
                    cmd.Parameters.AddWithValue("@FomemaTestYear", candidateModel.FomemaTestYear);
                    cmd.Parameters.AddWithValue("@FomemaTestResult", candidateModel.FomemaTestResult);

                    // Child Table fields
                    cmd.Parameters.AddWithValue("@Covid19TestDate1", GetFormattedDateTime(candidateModel.Covid19TestDate1));
                    cmd.Parameters.AddWithValue("@Covid19TestDate2", GetFormattedDateTime(candidateModel.Covid19TestDate2));
                    cmd.Parameters.AddWithValue("@Covid19TestDate3", GetFormattedDateTime(candidateModel.Covid19TestDate3));
                    cmd.Parameters.AddWithValue("@Covid19TestDate4", GetFormattedDateTime(candidateModel.Covid19TestDate4));
                    cmd.Parameters.AddWithValue("@VaccineDose1Date", GetFormattedDateTime(candidateModel.VaccineDose1Date));
                    cmd.Parameters.AddWithValue("@VaccineDose2Date", GetFormattedDateTime(candidateModel.VaccineDose2Date));
                    cmd.Parameters.AddWithValue("@ClinicName", candidateModel.ClinicName);
                    cmd.Parameters.AddWithValue("@ClinicLocation", candidateModel.ClinicLocation);
                }

                cmd.Parameters.AddWithValue("@UpdateCandidateId", 0).Direction = ParameterDirection.Output; // output variable

                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@UpdateCandidateId"].Value);
                conn.Close();
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;

        }
        public static DateTime? GetFormattedDateTime(string dateTimeString)
        {
            return !string.IsNullOrEmpty(dateTimeString) ? Convert.ToDateTime(dateTimeString) : (DateTime?)null;
        }
        #endregion

    }
}