using Newtonsoft.Json;
using RankedReadyApi.Common.DataTransferObjects.Transaction;
using System.ComponentModel.DataAnnotations;

namespace RankedReadyApi.Common.Models.Transaction;

public class TransactionTransfer
{
    [JsonIgnore]
    public string ServerKey { get; set; }

    [JsonIgnore]
    public string Endpoint { get; set; }

    [JsonProperty(PropertyName = "profile_id")]
    [Display(Name = "Profile ID")]
    public int ProfileId { get; set; }

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

    public static TransactionTransfer GetTransactionTransfer(TransactionDto model)
    {
        var transaction = new TransactionTransfer();
        transaction.TranType = model.TranType;
        transaction.TranClass = model.TranClass;
        transaction.CartId = model.CartId;
        transaction.CartCurrency = model.CartCurrency;
        transaction.CartAmount = model.CartAmount;
        transaction.CartDescription = model.CartDescription;
        transaction.PaypageLang = model.PaypageLang;
        transaction.HideShipping = model.HideShipping;
        transaction.IsFramed = model.IsFramed;
        transaction.ReturnURL = model.ReturnURL;
        transaction.CallbackURL = model.CallbackURL;
        transaction.ServerKey = model.ServerKey;
        transaction.Endpoint = model.Endpoint;
        return transaction;
    }
}
