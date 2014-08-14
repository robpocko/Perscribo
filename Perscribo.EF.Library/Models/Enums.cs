namespace Perscribo.EF.Library.Models
{
    public enum StateName : int
    {
        Victoria         = 1,
        New_South_Wales  = 2,
        Queensland       = 3,
        South_Australia  = 4,
        Tasmania         = 5,
        ACT              = 6
    }

    public enum PositionType : int
    {
        Permanent  = 1,
        Contract   = 2,
        Fixed_Term = 3
    }

    public enum SalaryType : int
    {
        Per_Hour       = 1,
        Per_Day        = 2,
        Salary         = 3,
        Salary_Package = 4
    }

    public enum RoleStatus : int
    {
        Applied_For          = 1,
        Agent_Called         = Applied_For + 1,
        Consultant_Interview = Agent_Called + 1,
        Resume_To_Company    = Consultant_Interview + 1,
        Company_Interview    = Resume_To_Company + 1,
        Offer_Received       = Company_Interview + 1,
        Closed               = Offer_Received + 1
    }
}
