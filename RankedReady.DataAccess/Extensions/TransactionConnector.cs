using Newtonsoft.Json;
using RankedReadyApi.Common.Models.Transaction;
using RestSharp;


namespace RankedReady.DataAccess.Extensions;

public class TransactionConnector
{
    public TransactionConnector()
    {
    }

    public Transaction_Response Send(TransactionTransfer transaction)
    {
        string base_url = transaction.Endpoint; // "https://secure.paytabs.com/";
        string payment_url = base_url + "payment/request";

        string body = JsonConvert.SerializeObject(transaction);

        var client = new RestClient(payment_url);

        var request = new RestRequest();
        request.Method = Method.Post;
        request.AddHeader("authorization", transaction.ServerKey);
        request.AddParameter("text/plain", body, ParameterType.RequestBody);

        var response = client.Execute(request);

        Transaction_Response tran_res = JsonConvert.DeserializeObject<Transaction_Response>(response.Content);
        return tran_res;
    }
}
