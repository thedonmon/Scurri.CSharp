using Scurri.Client.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scurri.Client
{
    public interface IRestApiClient
    {
        /// <summary>
        /// The Carriers API allows you to query Scurri for the carriers that are enabled in your account.
        /// The result contains the identifier for each carrier, which you can use in the rest of the API calls, whenever a Carrier needs to be specified. 
        /// The identifier will never change, so this can be called once to get the values.
        /// </summary>
        /// <returns></returns>
        Task<List<Carrier>> GetCarriersAsync();
        /// <summary>
        /// The CarrierServices API allows you to retrieve a list of services that have been enabled in your account.
        /// The result contains the identifier for each service, which you can use in the rest of the API calls, whenever a Service needs to be specified.
        /// </summary>
        /// <param name="enhancements">Whether to include enhancements in the response Default: false</param>
        /// <param name="package_types">Whether to include package types in the response Default: false</param>
        /// <returns></returns>
        Task<List<CarrierService>> GetCarrierServicesAsync(bool enhancements = false, bool package_types = false);
        /// <summary>
        /// The Warehouses API allows you to retrieve a list of warehouses that you have access to (within a company).
        /// </summary>
        /// <returns></returns>
        Task<List<Warehouse>> GetWarehousesAsync();
        /// <summary>
        /// You can get use this API call to retrieve the list of consignments in Scurri.Pagination is provided via the offset and limit GET parameters.
        /// For the offset, we don't use an integer value but instead use the last identifier of the current batch. 
        /// The next value returned with the response body contains a URL you can use to retrieve the next batch.
        /// </summary>
        /// <param name="offset">The offset for any pagination. This is the string identifier of the last consignment entry in the current batch.</param>
        /// <param name="limit"></param>
        /// <param name="identifier"></param>
        /// <param name="status">Possible values:  Unallocated , Allocated , Printed , Manifested , Despatched , Delivered , Exception , Cancelled </param>
        /// <returns></returns>
        Task<PaginationResult<Consignment>> GetConsignmentsAsync(string offset = "", int limit = 10, string identifier = null, string status = null);
        Task<List<Consignment>> GetConsignmentsPagedAsync(int pagesToIterate = 0, string offset = "", int limit = 10, string identifier = null, string status = null);
        /// <summary>
        /// You can use this API call to get all details for the specified consignment.For instance, this is how you can query the status of a consignment and get any error messages from the allocation phase.
        /// </summary>
        /// <param name="consignment_id">The consignment identifier for the consignment</param>
        /// <returns></returns>
        Task<Consignment> GetConsignmentAsync(string consignment_id);
        /// <summary>
        /// You can use this API call to create one or more new consignments. In both cases, you have to specify a list in the body.
        /// Rules are always executed against your consignment data during the allocation phase.Another thing to keep in mind is that, in the case of batch creating consignments, Scurri will run any allocation processes asynchronously.
        /// As a result, a label will not be available straight away. If your request contains data only for a single consignment then the allocation happens synchronously.
        /// If you need a way to associate your internal packages with Scurri packages, please use the key reference that is available for each package.The reference don't need to be unique per company or per item.
        /// Finally, some carriers support enhancements and carrier-specific options which must be specified in the "options" field, in example: Signed Consignments ("signed") or Package Type ("package_type").
        /// </summary>
        /// <param name="consigments">List of consignments to create</param>
        /// <returns></returns>
        Task<CreateObjectResponse> CreateConsignmentAsync(List<Consignment> consigments);
        /// <summary>
        /// You can use this API call to update the details of the specified consignment.The API does not support partial updates, so you have to specify all values, when using this API call.
        /// </summary>
        /// <param name="consigment"></param>
        /// <returns></returns>
        Task<Consignment> UpdateConsignmentAsync(Consignment consigment);
        /// <summary>
        /// You can use this API call to cancel a consignment. Scurri will notify, when necessary, the carrier to void the generated labels.
        /// </summary>
        /// <param name="consigmentid">The consignment identifier for the consignment</param>
        /// <returns></returns>
        Task<HttpResponseMessage> CancelConsignmentAsync(string consigmentid);
        /// <summary>
        /// You can use this API call to get the label and any customs invoice documents required for a specific consignment, if required.
        /// The resulting documents are base64-encoded and they can be either in PDF format or in PNG. In the former case, there is one PDF returned with multiple labels concatenated in a single document (one label per page). 
        /// In the latter case, we cannot concatenate PNG files together, so a ZIP file is returned instead that contains all files.
        /// Note that setting documenttype to application/x-zebra-zpl is only impacting labels, not invoices - they remain PDF as they are A/4.
        /// ZPL Supported Carriers: An Post, Colissimo, Deutsche Post, DPD, DPD Germany, DPD Ireland, DX, Fastway, Generic Carrier, Hermes, Interlink, ITD Carrier, P2P Trakpak, ParcelForce, PostNL, Royal Mail, Spring Global, TNT, Tuffnells, UKMail, Yodel
        /// </summary>
        /// <param name="consignmentid">The identifier of the consignment.</param>
        /// <param name="documenttype">The file format of the result. The available values are application/pdf, image/png and application/x-zebra-zpl.</param>
        /// <param name="labelquantity">The number of copies of the label you want. Default: 1</param>
        /// <param name="invoice_quantity">The number of copies for the customs invoice document you want. Default: 3.</param>
        /// <returns></returns>
        Task<ConsignmentDocument> GetConsignmentDocumentsAsync(string consignmentid, string documenttype = "application/pdf", int labelquantity = 1, int invoice_quantity = 3);
        /// <summary>
        /// You can use this API call to mark a list of consignments for a specific carrier and specific warehouse as manifested.
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        Task<ManifestResponse> CreateManifestAsync(Manifest manifest);
        /// <summary>
        /// The response contains the PDF documents as a base64-encoded string.
        /// </summary>
        /// <param name="manifest_id">The identifier of the manifest, as returned by the create API call.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetManifestDocumentsAsync(string manifest_id);
    }
}