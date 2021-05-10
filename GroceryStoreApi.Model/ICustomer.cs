
namespace GroceryStoreApi.Model
{
    /// <summary>
    /// Customer
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// First Name of the customer
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Last Name of the customer
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Gender of the customer
        /// </summary>
        string Gender { get; set; }
        /// <summary>
        /// Return boolean that represents customer data is valid or not
        /// </summary>
        /// <returns></returns>
        bool IsValid();
        /// <summary>
        /// Return boolean that represents customer data is valid or not
        /// </summary>
        /// <returns></returns>
        void Update(ICustomer updated);
    }
}
