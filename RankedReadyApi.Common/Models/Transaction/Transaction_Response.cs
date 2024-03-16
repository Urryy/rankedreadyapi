namespace RankedReadyApi.Common.Models.Transaction;

public class Transaction_Response
{
    public string tran_ref { get; set; }
    public string redirect_url { get; set; }

    public string tran_type { get; set; }
    public string cart_id { get; set; }
    public string cart_description { get; set; }
    public string cart_currency { get; set; }
    public string cart_amount { get; set; }
    public string tran_currency { get; set; }
    public string tran_total { get; set; }
    public string callback { get; set; }

    public bool IsSuccess()
    {
        if (String.IsNullOrWhiteSpace(tran_ref) || String.IsNullOrWhiteSpace(redirect_url))
        {
            return false;
        }

        return true;
    }
}
