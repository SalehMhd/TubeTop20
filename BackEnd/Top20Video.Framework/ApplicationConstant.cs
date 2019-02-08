using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Video.Framework
{
    public class ApplicationConstant
    {
        public const string New = "Request to add new category";
        public const string DateFormatJs = "mm/dd/yyyy";
        public const string DateFormat = "MM/dd/yyyy";
        public const string DateFormatDisplay = "dd MMM yyyy";
        public const string AddDateFormatDisplay = "dd M yyyy";
        public const string NewMaterial = "Request to add new material";
        public const string RFQIDFormate = "000000";
    }
    public class UserRoleName
    {
        public const string SuperAdmin = "Super Admin";
        public const string CompanyAdmin = "Company Admin";
        public const string AdminUser = "Admin User";
        public const string User = "User";
    }

    public static class RfqStagesName
    {
        public const string MaterialControl = "Material Controller";
        public const string TechnicalReview = "Technical Reviewers";
        public const string InvitationToBid = "Invitation to Bid";
        public const string BidSubmission = "Bid submission";
        public const string BidClarification = "Bid clarification";
        public const string TechnicalApproval = "Technical Approvers";
        public const string SingleSourceApproval = "Single Source Approvers";
        public const string CommercialApproval = "Commercial Approvers";
        public const string Award = "Award";
        public const string NonAward = "Non-award";
        public const string VariationInScope = "Variation in scope";
        public const string CloseOut = "Close-Out";
    }

    public static class ApplicationIcons
    {
        public const string Edit = "~/Content/images/edit.png";
        public const string Delete = "~/Content/images/delete.png";
        public const string Detail = "~/Content/images/detail.png";
        public const string Download = "~/Content/images/download.png";
        public const string Add = "~/Content/images/add.png";
        public const string Search = "~/Content/images/search.png";
        public const string Export = "~/Content/images/export.png";
        public const string PDF = "~/Content/images/pdf.png";
        public const string Xls = "~/Content/images/xls.png";
        public const string Email = "~/Content/images/email.png";
        public const string Print = "~/Content/images/print.png";

        public const string Workflow = "~/Content/images/interface.png";
        public const string SaveButton = "~/Content/images/download.png";
        public const string CancelButton = "~/Content/images/detail.png";
        public const string DeleteButton = "~/Content/images/remove.png";
        public const string EditButton = "~/Content/images/edit2.png";

    }


    public class TemplateName
    {
        public const string DeleteAccount = "Delete Account";
        public const string ForgotPassword = "Forgot Password";
        public const string ContactUs = "Contact Us";
        public const string MerchantRegistration = "Merchant Registration";
        public const string CompanyAdminRegistration = "Company Admin Registration";
        public const string UserRegistration = "User Registration";
        public const string RFQStageCompletedNotification = "RFQ Stage Completed Notification";
        public const string RFQStageNotification = "RFQ Stage Notification";
        public const string VendorRegsitration = "Vendor Registration";
        public const string VisitNotification = "Visit Notification";
        public const string RaiseClarification = "Raise Clarification";
        public const string ResponseClarification = "Response Clarification";
        public const string RFQBidClarificationStage = "RFQ Bid Clarification Stage";
        public const string RFQInitated = "RFQ Initated";
        public const string RFQUpdated = "RFQ Updated";
        public const string RFQStageRejected = "RFQ Stage Rejected Notification";
        public const string RFQAward = "RFQ Award Stage Notification";
        public const string RFQInvitationToBid = "RFQ Invitation to Bid Stage Notification";
        public const string RFQNonAward = "RFQ Non Award Stage Notification";
        public const string RFQCloseOut = "RFQ Close Out Stage Notification";
        public const string RenewalReminder = "Consolidated Renewal Reminder";
        public const string CompanyLicenseApplication = "Company License Application";
        public const string CompanyLicenseConfirmation = "Company License Confirmation";
        public const string Promotion = "Promotion Template";

        public const string ComapanyRegistrationNotification = "New Registration Notification";
        public const string ComapanyRegistrationConformation = "New Registration Conformation Notification";

        public const string MessageNotification = "Message Notification";
        public const string ResponseMessageNotification = "Response Message Notification";
    }

    public class TemplateNameSubject
    {
        public const string DeleteAccount = "Delete Account";
        public const string ForgotPassword = "Forgot Password";
        public const string ContactUs = "Contact Us";
        public const string MerchantRegistration = "Merchant Registration";
        public const string CompanyAdminRegistration = "Company Admin Registration";
        public const string UserRegistration = "User Registration";
        public const string RFQStageCompletedNotification = "Notification: RFQ Stage Completed ";
        public const string RFQStageNotification = "Notification: RFQ Stage";
        public const string VendorRegsitration = "Vendor Registration";
        public const string VisitNotification = "Visit Notification";
        public const string RaiseClarification = "Raise Clarification";
        public const string ResponseClarification = "Response Clarification";
        public const string RFQBidClarificationStage = "RFQ Bid Clarification Stage";
        public const string RFQInitated = "RFQ Initated";
        public const string RFQUpdated = "RFQ Updated";
        public const string RFQStageRejected = "RFQ Stage Rejected Notification";
        public const string RFQAward = "RFQ Award Stage Notification";
        public const string RFQInvitationToBid = "RFQ Invitation to Bid Stage Notification";
        public const string RFQNonAward = "RFQ Non Award Stage Notification";
        public const string RFQCloseOut = "RFQ Close Out Stage Notification";
        public const string RenewalReminder = "Consolidated Renewal Reminder";

        public const string CompanyLicenseApplication = "New License Invoice Generated";
        public const string CompanyLicenseConfirmation = "Invoice Paid Successfully";

        public const string Promotion = "User Limit Exeeding Notification";
        public const string ComapanyRegistrationNotification = "New Registration Application";
        public const string ComapanyRegistrationConformation = "New Company Registration Conformed";
        public const string MessageNotification = "Message Notification";
        public const string ResponseMessageNotification = "Response Message Notification";
    }

    public class EntityType
    {
        public const string AdminUser = "AdminUser";
        public const string BusinessUnitMaster = "BusinessUnitMaster";
        public const string CategoryMaster = "CategoryMaster";
        public const string RFQStageWorkflow = "RFQStageWorkflow";
        public const string Company = "Company";
        public const string CompanyUser = "CompanyUser";
        public const string BidDetail = "BidDetail";
        public const string ContentTypes = "ContentTypes";
        public const string Country = "Country";
        public const string DepartmentMaster = "DepartmentMaster";
        public const string StatusMaster = "StatusMaster";
        public const string DocumentTemplates = "DocumentTemplates";
        public const string EventLog = "EventLog";
        public const string HomePagecontentDetails = "HomePagecontentDetails";
        public const string InitiateRFQ = "InitiateRFQ";
        public const string RFQBid = "RFQBid";
        public const string MenuMaster = "MenuMaster";
        public const string Attachments = "Attachments";
        public const string Notification = "Notification";
        public const string NotificationType = "NotificationType";
        public const string RFQAttachment = "RFQAttachment";
        public const string Permission = "Permission";
        public const string PermissionMaster = "PermissionMaster";
        public const string Role = "Role";
        public const string UserPermission = "UserPermission";
        public const string UserRole = "UserRole";
        public const string LicensePlan = "LicensePlan";
        public const string Users = "Users";
        public const string Vendor = "Vendor";
        public const string RFQOrderDetail = "RFQOrderDetail";
        public const string AdminMessage = "AdminMessage";
        public const string Material = "Material";
        public const string CompanyLicenseInvoice = "CompanyLicenseInvoice";
        public const string RFQMaterial = "RFQMaterial";
        public const string SizeUnitMaster = "SizeUnitMaster";
        public const string RFQFavourite = "RFQFavourite";
        public const string TransactionHistory = "TransactionHistory";
        public const string CompanyLicense = "CompanyLicense";
        public const string InvitationBidDetail = "InvitationBidDetail";
        public const string RFQMessages = "RFQMessages";
        public const string UserActivity = "UserActivity";
        public const string LicenseCategory = "LicenseCategory";
        public const string RaiseClarification = "RaiseClarification";
        public const string RFQStage = "RFQStage";
        public const string ComapnyRegistration = "ComapnyRegistration";
    }


}

