using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RankedReadyApi.Common.Entities;

using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
public class Transaction
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public Guid AccountId { get; set; }
    [JsonIgnore]
    public string Email { get; set; }

    [JsonIgnore]
    public bool? TriedToPay { get; set; }

    [JsonIgnore]
    public bool? IsSucceed { get; set; }

    [JsonIgnore]
    public string? Tran_Ref { get; set; }

    [JsonIgnore]
    public bool? IsValid_Signature { get; set; }


    [JsonProperty(PropertyName = "profile_id")]
    [Display(Name = "Profile ID")]
    public int ProfileId { get; set; }

    [JsonIgnore]
    public string ServerKey { get; set; }

    [JsonIgnore]
    public string Endpoint { get; set; }


    [JsonProperty(PropertyName = "tran_type")]
    [Display(Name = "Transaction Type")]
    public string TranType { get; set; }

    [JsonProperty(PropertyName = "tran_class")]
    [Display(Name = "Transaction Class")]
    public string TranClass { get; set; }

    [JsonProperty(PropertyName = "cart_id")]
    [Display(Name = "Cart ID")]
    public string CartId { get; set; }

    [JsonProperty(PropertyName = "cart_currency")]
    public string CartCurrency { get; set; }

    [JsonProperty(PropertyName = "cart_amount")]
    public float CartAmount { get; set; }

    [JsonProperty(PropertyName = "cart_description")]
    public string CartDescription { get; set; }


    [JsonProperty(PropertyName = "paypage_lang")]
    [Display(Name = "Language")]
    public string PaypageLang { get; set; }


    [JsonProperty(PropertyName = "hide_shipping")]
    [Display(Name = "Hide shipping?")]
    public bool HideShipping { get; set; }

    [JsonProperty(PropertyName = "framed")]
    public bool IsFramed { get; set; }

    [JsonProperty(PropertyName = "return")]
    [Display(Name = "Return URL")]
    public string ReturnURL { get; set; }

    [JsonProperty(PropertyName = "callback")]
    [Display(Name = "Callback URL")]
    public string CallbackURL { get; set; }

    [JsonProperty(PropertyName = "date_transaction")]
    [Display(Name = "Date Transaction")]
    public DateTime DateTransaction { get; set; } = DateTime.UtcNow;

    public User User { get; set; }

    public Transaction(Guid userId, Guid accountId, string email, bool? triedToPay, bool? isSucceed, string? tran_Ref, bool? isValid_Signature, int profileId, string serverKey, string endpoint, string tranType, string tranClass, string cartId, string cartCurrency, float cartAmount, string cartDescription, string paypageLang, bool hideShipping, bool isFramed, string returnURL, string callbackURL)
    {
        UserId = userId;
        AccountId = accountId;
        Email = email;
        TriedToPay = triedToPay;
        IsSucceed = isSucceed;
        Tran_Ref = tran_Ref;
        IsValid_Signature = isValid_Signature;
        ProfileId = profileId;
        ServerKey = serverKey;
        Endpoint = endpoint;
        TranType = tranType;
        TranClass = tranClass;
        CartId = cartId;
        CartCurrency = cartCurrency;
        CartAmount = cartAmount;
        CartDescription = cartDescription;
        PaypageLang = paypageLang;
        HideShipping = hideShipping;
        IsFramed = isFramed;
        ReturnURL = returnURL;
        CallbackURL = callbackURL;
    }
}
