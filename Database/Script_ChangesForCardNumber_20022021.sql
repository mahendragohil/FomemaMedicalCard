alter table CandidateMaster add CandidateCardNumber varchar(50) unique;
GO
alter table CandidateMaster add CandidateCardNumberEncrypt varchar(500) unique;
GO
ALTER proc [dbo].[GetCandidateDetails]
@PageIndex int,
@PageSize int,
@SortCol nvarchar(255) = NULL,
@SortDir nvarchar(10) = 'asc',
@CandidateCardNumber varchar(50) = null,
@Name  nvarchar(255) = NULL,
@Age int = 0,
@NewPassportNo nvarchar(255) = NULL ,
@OldPassportNo nvarchar(255) = NULL ,
@CountryName nvarchar(255) = NULL ,
@FomemaTestYear int = 0 ,
@FomemaTestResult  nvarchar(255) = NULL,
@UserType int = 2,
@SearchPassportNo nvarchar(255) = NULL
as
begin
    Declare @FirstRec int, @LastRec int
    Set @FirstRec = (@PageIndex - 1) * @PageSize;
    Set @LastRec = @FirstRec + @PageSize;
   
   if @UserType = 1
       With CTE_Candidates as
    (
         Select ROW_NUMBER() over (order by 

		 case when (@SortCol = '' and (@SortDir='asc' or @SortDir=''))
            then M.CandidateId
        end asc,
        case when (@SortCol = '' and @SortDir='desc')
            then  M.CandidateId
        end desc,

		 case when (@SortCol = 'Name' and @SortDir='asc')
            then M.Name
        end asc,
        case when (@SortCol = 'Name' and @SortDir='desc')
            then  M.Name
        end desc,

		case when (@SortCol = 'DateofBirth' and @SortDir='asc')
            then M.DateofBirth
        end asc,
        case when (@SortCol = 'DateofBirth' and @SortDir='desc')
            then  M.DateofBirth
        end desc,

		case when (@SortCol = 'Age' and @SortDir='asc')
            then M.Age
        end asc,
        case when (@SortCol = 'Age' and @SortDir='desc')
            then  M.Age
        end desc,

		case when (@SortCol = 'NewPassportNo' and @SortDir='asc')
            then M.NewPassportNo
        end asc,
        case when (@SortCol = 'NewPassportNo' and @SortDir='desc')
            then  M.NewPassportNo
        end desc,

		case when (@SortCol = 'OldPassportNo' and @SortDir='asc')
            then M.OldPassportNo
        end asc,
        case when (@SortCol = 'OldPassportNo' and @SortDir='desc')
            then  M.OldPassportNo
        end desc,

		case when (@SortCol = 'CountryName' and @SortDir='asc')
            then M.CountryName
        end asc,
        case when (@SortCol = 'CountryName' and @SortDir='desc')
            then  M.CountryName
        end desc,

		case when (@SortCol = 'FomemaTestYear' and @SortDir='asc')
            then M.FomemaTestYear
        end asc,
        case when (@SortCol = 'FomemaTestYear' and @SortDir='desc')
            then  M.FomemaTestYear
        end desc,

		case when (@SortCol = 'FomemaTestResult' and @SortDir='asc')
            then M.FomemaTestResult
        end asc,
        case when (@SortCol = 'FomemaTestResult' and @SortDir='desc')
            then  M.FomemaTestResult
        end desc
		
		)  as RowNum,
         COUNT(*) over() as TotalCount, 
			M.CandidateId, 
			M.CandidateGuid,
			M.Name, 
			M.DateofBirth, 
			M.Age, 
			M.NewPassportNo, 
			M.OldPassportNo, 
			M.PictureURL, 
			M.CountryName, 
			M.FomemaTestYear, 
			M.FomemaTestResult,
			M.CandidateCardNumber,
			D.Covid19TestDate1, 
			D.Covid19TestDate2, 
			D.Covid19TestDate3, 
			D.Covid19TestDate4, 
			D.VaccineDose1Date, 
			D.VaccineDose2Date, 
			D.ClinicName, 
			D.ClinicLocation
FROM     CandidateMaster as M 
inner join CandidateDetails as D 
on  M.CandidateId = D.CandidateId

         where 
			 (@CandidateCardNumber IS NULL OR M.CandidateCardNumber like '%' + @CandidateCardNumber + '%') AND
			 (@Name IS NULL OR M.Name like '%' + @Name + '%') AND
			  (@Age = 0 OR M.Age = @Age) AND
			   (@NewPassportNo IS NULL OR M.NewPassportNo like '%' + @NewPassportNo + '%') AND
			    (@OldPassportNo IS NULL OR M.OldPassportNo like '%' + @OldPassportNo + '%') AND
				 (@CountryName IS NULL OR M.CountryName like '%' + @CountryName + '%') AND
				  (@FomemaTestYear = 0 OR M.FomemaTestYear  = @FomemaTestYear ) AND
				   (@FomemaTestResult = '' OR M.FomemaTestResult =  @FomemaTestResult ) AND
				    (M.IsDeleted = 0)
    )

    Select *
    from CTE_Candidates
    where RowNum > @FirstRec and RowNum <= @LastRec;

	
	else
	
	 With CTE_Candidates as
    (
         Select ROW_NUMBER() over (order by 
		  case when (@SortCol = '' and (@SortDir='asc' or @SortDir=''))
            then M.CandidateId
        end asc,
        case when (@SortCol = '' and @SortDir='desc')
            then  M.CandidateId
        end desc,

		 case when (@SortCol = 'Name' and @SortDir='asc')
            then M.Name
        end asc,
        case when (@SortCol = 'Name' and @SortDir='desc')
            then  M.Name
        end desc,

		case when (@SortCol = 'DateofBirth' and @SortDir='asc')
            then M.DateofBirth
        end asc,
        case when (@SortCol = 'DateofBirth' and @SortDir='desc')
            then  M.DateofBirth
        end desc,

		case when (@SortCol = 'Age' and @SortDir='asc')
            then M.Age
        end asc,
        case when (@SortCol = 'Age' and @SortDir='desc')
            then  M.Age
        end desc,

		case when (@SortCol = 'NewPassportNo' and @SortDir='asc')
            then M.NewPassportNo
        end asc,
        case when (@SortCol = 'NewPassportNo' and @SortDir='desc')
            then  M.NewPassportNo
        end desc,

		case when (@SortCol = 'OldPassportNo' and @SortDir='asc')
            then M.OldPassportNo
        end asc,
        case when (@SortCol = 'OldPassportNo' and @SortDir='desc')
            then  M.OldPassportNo
        end desc,

		case when (@SortCol = 'CountryName' and @SortDir='asc')
            then M.CountryName
        end asc,
        case when (@SortCol = 'CountryName' and @SortDir='desc')
            then  M.CountryName
        end desc,

		case when (@SortCol = 'FomemaTestYear' and @SortDir='asc')
            then M.FomemaTestYear
        end asc,
        case when (@SortCol = 'FomemaTestYear' and @SortDir='desc')
            then  M.FomemaTestYear
        end desc,

		case when (@SortCol = 'FomemaTestResult' and @SortDir='asc')
            then M.FomemaTestResult
        end asc,
        case when (@SortCol = 'FomemaTestResult' and @SortDir='desc')
            then  M.FomemaTestResult
        end desc
		 )  as RowNum,
         COUNT(*) over() as TotalCount, 
			M.CandidateId, 
			M.CandidateGuid,
			M.Name, 
			M.DateofBirth, 
			M.Age, 
			M.NewPassportNo, 
			M.OldPassportNo, 
			M.PictureURL, 
			M.CountryName, 
			M.FomemaTestYear, 
			M.FomemaTestResult,
			M.CandidateCardNumber,
			D.Covid19TestDate1, 
			D.Covid19TestDate2, 
			D.Covid19TestDate3, 
			D.Covid19TestDate4, 
			D.VaccineDose1Date, 
			D.VaccineDose2Date, 
			D.ClinicName, 
			D.ClinicLocation
FROM     CandidateMaster as M 
inner join CandidateDetails as D 
on  M.CandidateId = D.CandidateId

         where 
			   ((@SearchPassportNo != '' AND M.NewPassportNo like '%' + @SearchPassportNo + '%') OR
			    (@SearchPassportNo  != '' AND M.OldPassportNo like '%' + @SearchPassportNo + '%')) AND
				  (M.IsDeleted = 0)				 
    )

    Select *
    from CTE_Candidates
    where RowNum > @FirstRec and RowNum <= @LastRec
	
end;
GO
ALTER PROCEDURE [dbo].[IUDSaveCandidateDetails]
	-- Add the parameters for the stored procedure here
	@DataOperationMode int, -- Insert = 10,Update = 20,Delete = 30
	@CandidateId int = null,
	@CandidateCardNumber varchar(50),
	@CandidateCardNumberEncrypt varchar(200),
	@CandidateGuid uniqueidentifier = null,
	@Name varchar(500) = null,
	@DateofBirth datetime = null,
	@Age int = null,
	@NewPassportNo varchar(50)= null,
	@OldPassportNo varchar(50)= null,
	@PictureURL varchar(max) = null,
	@CountryName varchar(200) = null,
	@IsDeleted bit = null,
	@RedirectedPath varchar(500) = null,
	@FomemaTestYear int = null,
	@FomemaTestResult varchar(30)= null,
	@CreatedBy int= null,
	@CreatedDate datetime = null,
	@ModifiedBy int = null,
	@ModifiedDate datetime = null,
	--- Child table fields
	@Covid19TestDate1 datetime = null,
	@Covid19TestDate2 datetime = null, 
	@Covid19TestDate3 datetime = null,
	@Covid19TestDate4 datetime = null,
	@VaccineDose1Date datetime = null,
	@VaccineDose2Date datetime = null,
	@ClinicName       varchar(200)= null,
	@ClinicLocation   varchar(300)= null,
	@UpdateCandidateId int output
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--DECLARE @UpdateCandidateId INT; --set candidate id
	Set @UpdateCandidateId = @CandidateId;
	if(@DataOperationMode = 10)
	Begin
		-- Insert statements for procedure here
		INSERT INTO CandidateMaster 
		Output Inserted.CandidateId
		values(@CandidateGuid,@Name,@DateofBirth,@Age,@NewPassportNo,@OldPassportNo,@PictureURL,@CountryName,@IsDeleted,
		@RedirectedPath,@FomemaTestYear,@FomemaTestResult,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,@CandidateCardNumber,@CandidateCardNumberEncrypt);
		Set @UpdateCandidateId = SCOPE_IDENTITY();
		Insert Into CandidateDetails
		values(@UpdateCandidateId,@Covid19TestDate1,@Covid19TestDate2,@Covid19TestDate3,@Covid19TestDate4,@VaccineDose1Date,@VaccineDose2Date,
			   @ClinicName,@ClinicLocation)
	End
	Else if(@DataOperationMode = 20)
	Begin
		Update CandidateMaster set
			Name = @Name,DateofBirth=@DateofBirth,Age=@Age,NewPassportNo=@NewPassportNo,OldPassportNo=@OldPassportNo,
			PictureURL=@PictureURL,CountryName=@CountryName,RedirectedPath=@RedirectedPath,FomemaTestYear=@FomemaTestYear,
			FomemaTestResult=@FomemaTestResult,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate
		where CandidateId = @CandidateId

		Update CandidateDetails set
			Covid19TestDate1=@Covid19TestDate1,Covid19TestDate2=@Covid19TestDate2,
			Covid19TestDate3=@Covid19TestDate3,Covid19TestDate4=@Covid19TestDate4,VaccineDose1Date=@VaccineDose1Date,
			VaccineDose2Date=@VaccineDose2Date,ClinicName=@ClinicName,ClinicLocation=@ClinicLocation
		where CandidateId = @CandidateId		
	End
	Else if(@DataOperationMode = 30)
	Begin
		--- delete
		Update CandidateMaster set
		IsDeleted = 1
		where CandidateId = @CandidateId
	END
	return @UpdateCandidateId;
	--Select 10 as Result;
End;
GO
ALTER PROCEDURE [dbo].[CandidateMedicalCardGetData]
(
@CandidateCardNumber varchar(50)
)
AS
SELECT    candidatemst.candidateid, 
              candidatemst.NAME, 
              candidatemst.age, 
              candidatemst.newpassportno, 
              candidatemst.oldpassportno, 
              candidatemst.pictureurl, 
              candidatemst.countryname, 
              candidatemst.isdeleted, 
              candidatemst.redirectedpath, 
			  candidatemst.CandidateCardNumber, 
              candidatedet.srno, 
              candidatedet.candidateid, 
              candidatedet.covid19testdate1, 
              candidatedet.covid19testdate2, 
              candidatedet.covid19testdate3, 
              candidatedet.covid19testdate4, 
              candidatedet.vaccinedose1date, 
              candidatedet.vaccinedose2date, 
              candidatedet.clinicname, 
              candidatedet.cliniclocation 
    FROM      candidatemaster CandidateMst 
    LEFT JOIN candidatedetails CandidateDet 
    ON        candidatedet.candidateid = candidatemst.candidateid 
    WHERE    candidatemst.CandidateCardNumberEncrypt = @CandidateCardNumber and IsDeleted = 0;

GO

create table FomemaMedicalCardList
(
Id int primary key identity(1,1),
CardNumber varchar(50) not null unique,
CardNumberEncrypt varchar(200) not null unique,
CardUrl varchar(500) not null unique
)
GO

create procedure sp_GetCardListRecord
(
@CardNumber varchar(50)
)
as
begin
select * from FomemaMedicalCardList where CardNumber = @CardNumber;
end;
GO

create procedure sp_GetCardMasterRecord
(
@CardNumber varchar(50)
)
as
begin
select * from CandidateMaster where CandidateCardNumber = @CardNumber;
end;
GO
