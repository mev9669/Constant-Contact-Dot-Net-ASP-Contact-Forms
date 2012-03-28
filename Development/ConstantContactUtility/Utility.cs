using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ConstantContactBO;
using ConstantContactUtility.Components;

namespace ConstantContactUtility
{
    /// <summary>
    /// Provides utility methods to create a new Contact, update an existing one, etc.
    /// 
    /// </summary>
    public class Utility
    {
        #region Constants
        /// <summary>
        /// Constant Contact server error message for error code 404
        /// </summary>
        private const string WebExceptionCode404Message = "The remote server returned an error: (404) Not Found.";
        /// <summary>
        /// Constant Contact server error message for error code 403
        /// </summary>
        private const string WebExceptionCode403Message = "The remote server returned an error: (403) Forbidden.";
        #endregion

        #region User authentication
        /// <summary>
        /// Verify user authentication
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <exception cref="ConstantAuthenticationException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>
        public static void IsValidUserAuthentication(AuthenticationData authenticationData)
        {
            ValidateAuthenticationData(authenticationData);

            try
            {
                // try to access the Service Document resource
                // it will throw a WebException if Constant Contact credentials are invalid

                GetResponseStream(new Uri(authenticationData.AccountServiceDocumentUri), authenticationData);
            }
            catch (Exception e)
            {
                throw new ConstantAuthenticationException("Account authentication failed", e,
                                                          authenticationData.Username);
            }
        }
        #endregion


        #region Contact - Retrieve entire collection - 
        /// <summary>
        /// Retrieves the first chunk collection of Contacts that the server provides        
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <remarks>Constant Contact server provides paged collections</remarks>
        /// <param name="nextChunkId">Link to the next chunk of data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>The collection of Contacts</returns>
        public static IList<Contact> GetContactCollection(AuthenticationData authenticationData, out string nextChunkId)
        {
            return GetContactCollection(authenticationData, null, out nextChunkId);
        }

        /// <summary>
        /// Retrieves the collection of Contacts returned by server at the current chunk Id
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <remarks>Constant Contact server provides paged collections</remarks>
        /// <param name="currentChunkId">Link to the current chunk data</param>
        /// <param name="nextChunkId">Link to the next chunk of data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>The collection of Contacts</returns>
        public static IList<Contact> GetContactCollection(AuthenticationData authenticationData, string currentChunkId, out string nextChunkId)
        {
            ValidateAuthenticationData(authenticationData);

            string currentAddress = String.Format(CultureInfo.InvariantCulture, "{0}{1}",
                                                  authenticationData.AccountContactsUri, currentChunkId);

            Stream stream = Stream.Null;
            try
            {
                // get the response stream
                stream = GetResponseStream(new Uri(currentAddress), authenticationData);

                // parse the stream and get a collection of Contacts
                return ContactComponent.GetContactCollection(stream, out nextChunkId);
            }
            catch (Exception e)
            {
                throw new ConstantException(e.Message, e);
            }
            finally
            {
                // close the response stream
                stream.Close();
            }
        }
        #endregion

        #region Contact - Search -
        /// <summary>
        /// Retrieves the first chunk collection of Contacts that match specified Email Addresses
        /// </summary>
        /// <remarks>Constant Contact server provides paged collections</remarks>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="emailAddresses">One or more Email Addresses</param>
        /// <param name="nextChunkId">Link to the next chunk of data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>The collection of Contacts</returns>
        public static IList<Contact> SearchContactByEmail(AuthenticationData authenticationData, IEnumerable<string> emailAddresses, out string nextChunkId)
        {
            ValidateAuthenticationData(authenticationData);

            return SearchContactByEmail(authenticationData, emailAddresses, null, out nextChunkId);
        }

        /// <summary>
        /// Retrieves the collection of Contacts that match specified Email Addresses, returned by the server at current chunk Id.
        /// Entire collection of Contacts will be returned if no Email Address is specified
        /// </summary>
        /// <remarks>Constant Contact server provides paged collections</remarks>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="emailAddresses">One or more Email Addresses</param>
        /// <param name="currentChunkId">Link to the current chunk data</param>
        /// <param name="nextChunkId">Link to the next chunk of data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>The collection of Contacts</returns>
        public static List<Contact> SearchContactByEmail(AuthenticationData authenticationData, IEnumerable<string> emailAddresses, string currentChunkId, out string nextChunkId)
        {
            if (null == emailAddresses)
            {
                throw new ArgumentNullException("emailAddresses");
            }

            // create the Uri address with the Email address query
            StringBuilder uriAddress = new StringBuilder();
            uriAddress.Append(authenticationData.AccountContactsUri);
            uriAddress.Append("?");
            // loop the Email Address and create the query
            foreach (string email in emailAddresses)
            {
                uriAddress.AppendFormat("email={0}&", HttpUtility.UrlEncode(email.ToLower(CultureInfo.CurrentCulture)));
            }
            // remove the last '&' character from the query
            uriAddress.Remove(uriAddress.Length - 1, 1);

            string currentAddress = String.Format(CultureInfo.InvariantCulture, "{0}{1}",
                                                  uriAddress, currentChunkId);

            Stream stream = Stream.Null;
            try
            {
                // get the response stream
                stream = GetResponseStream(new Uri(currentAddress), authenticationData);

                // parse the stream and get a collection of Contacts
                return ContactComponent.GetContactCollection(stream, out nextChunkId);
            }
            catch (Exception e)
            {
                throw new ConstantException(e.Message, e);
            }
            finally
            {
                // close the response stream
                stream.Close();
            }
        }
        #endregion

        #region Contact - Retrieve details -
        /// <summary>
        /// Retrieve an individual Contact by its Id
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="id">Contact Id</param>        
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server or if no Contact with specified Id exists
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>Contact with specified Id</returns>
        public static Contact GetContactDetailsById(AuthenticationData authenticationData, string id)
        {
            ValidateAuthenticationData(authenticationData);

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Contact Id cannot be null or empty", "id");
            }
            
            // create the URI for specified Contact Id
            string completeUri = String.Format(CultureInfo.InvariantCulture, "{0}/{1}",
                                               authenticationData.AccountContactsUri, id);

            // get the response stream
            Stream stream = Stream.Null;
            try
            {
                stream = GetResponseStream(new Uri(completeUri), authenticationData);

                // parse the stream and obtain a Contact object
                return ContactComponent.GetContactDetails(stream);
            }
            catch (Exception e)
            {
                if (string.Compare(e.Message, WebExceptionCode404Message) == 0)
                {
                    throw new ConstantException(String.Format(CultureInfo.InvariantCulture,
                                                              "Contact with Id '{0}' does not exist.", id));
                }

                throw new ConstantException(e.Message, e);
            }
            finally
            {
                // close the response stream
                stream.Close();
            }
        }
        #endregion

        #region Contact - Create new -
        /// <summary>
        /// Create a New Contact
        /// </summary>        
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="contact">Contact to be created</param>        
        /// <remarks>The POST data presents only values for EmailAddress, FirstName, LastName, OptInSource and ContactLists elements</remarks>        
        /// <exception cref="ArgumentNullException">Thrown if specified Contact is null</exception>
        /// <exception cref="ArgumentException">Thrown if E-mail Address of specified Contact is null or empty</exception>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server or if specified Contact does not belongs to any list
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>Newly created Contact</returns>
        public static Contact CreateNewContact(AuthenticationData authenticationData, Contact contact)
        {
            ValidateAuthenticationData(authenticationData);

            if (null == contact)
            {
                throw new ArgumentNullException("contact");
            }

            if (string.IsNullOrEmpty(contact.EmailAddress))
            {
                throw new ArgumentException("Contact E-mail Address cannot be null or empty", "contact");
            }

            if (null == contact.ContactLists
                || contact.ContactLists.Count == 0)
            {
                throw new ConstantException("Contact does not belongs to any contact list");
            }

            // get the Atom entry for specified Contact
            StringBuilder data = ContactComponent.CreateNewContact(contact, authenticationData.AccountContactListsUri);

            Stream stream = Stream.Null;
            try
            {
                // post the Atom entry at specified Uri and save the response stream
                stream = PostInformation(authenticationData, new Uri(authenticationData.AccountContactsUri),
                                         data.ToString());

                // return newly created Contact
                return ContactComponent.GetContactDetails(stream);
            }
            catch (Exception e)
            {
                throw new ConstantException(e.Message, e);
            }
            finally
            {
                // close the response stream
                stream.Close();
            }
        }

        /// <summary>
        /// POST the data at the specified Uri address and returns the response Stream
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="address">Uri address</param>
        /// <param name="data">Data to be send at specified Uri address</param>
        /// <returns>Response Stream</returns>
        private static Stream PostInformation(AuthenticationData authenticationData, Uri address, string data)
        {
            // set the Http request content type
            const string contentType = @"application/atom+xml";

            // send a Http POST request and return the response Stream
            return GetResponseStream(authenticationData, address, WebRequestMethods.Http.Post, contentType, data);
        }
        #endregion

        #region Contact - Update -
        /// <summary>
        /// Update a Contact using the full form. All Contact fields will be updated
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="contact">Contact to be updated</param>
        /// <exception cref="ArgumentNullException">Thrown if specified Contact is null</exception>
        /// <exception cref="ArgumentException">Thrown if Id or Email Address of specified Contact is null or empty</exception>        
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server, if no Contact with specified Id exists 
        /// or if Contact cannot be updated (it belongs to the Do-Not-Mail list)
        /// or if ApiKey, Username or Password are null or empty</exception>        
        public static void UpdateContactFullForm(AuthenticationData authenticationData, Contact contact)
        {
            UpdateContact(authenticationData, contact, true);
        }

        /// <summary>
        /// Update a Contact using the simplified form. Only the following fields will be updated: 
        /// EmailAddress, FirstName, LastName, MiddleName, HomePhone, Addr1, Addr2, Addr3,
        /// City, StateCode, StateName, CountryCode, CountryName, PostalCode, SubPostalCode
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="contact">Contact to be updated</param>
        /// <exception cref="ArgumentNullException">Thrown if specified Contact is null</exception>
        /// <exception cref="ArgumentException">Thrown if Id or Email Address of specified Contact is null or empty</exception>        
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server, if no Contact with specified Id exists 
        /// or if Contact cannot be updated (it belongs to the Do-Not-Mail list)
        /// or if ApiKey, Username or Password are null or empty</exception>        
        public static void UpdateContactSmallForm(AuthenticationData authenticationData, Contact contact)
        {
            UpdateContact(authenticationData, contact, false);
        }

        /// <summary>
        /// Update a Contact
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="contact">Contact to be updated</param>
        /// <param name="fullUpdate">True if all Contact fields will be update; False otherwise (only the following fields 
        /// will be updated: EmailAddress, FirstName, LastName, MiddleName, HomePhone, Addr1, Addr2, Addr3,
        /// City, StateCode, StateName, CountryCode, CountryName, PostalCode, SubPostalCode)</param>
        /// <exception cref="ArgumentNullException">Thrown if specified Contact is null</exception>
        /// <exception cref="ArgumentException">Thrown if Id or Email Address of specified Contact is null or empty</exception>        
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server, if no Contact with specified Id exists 
        /// or if Contact cannot be updated (it belongs to the Do-Not-Mail list)
        /// or if ApiKey, Username or Password are null or empty</exception>        
        private static void UpdateContact(AuthenticationData authenticationData, Contact contact, bool fullUpdate)
        {
            ValidateAuthenticationData(authenticationData);

            if (null == contact)
            {
                throw new ArgumentNullException("contact");
            }

            if (string.IsNullOrEmpty(contact.Id))
            {
                throw new ArgumentException("Contact Id cannot be null or empty", "contact");
            }

            if (string.IsNullOrEmpty(contact.EmailAddress))
            {
                throw new ArgumentException("Contact Email Address cannot be null or empty", "contact");
            }

            // create the URI for specified Contact Id
            string completeUri = String.Format(CultureInfo.InvariantCulture, "{0}{1}",
                                               AuthenticationData.HostAddress, contact.Link);

            // get the Atom entry for specified Contact
            StringBuilder data = ContactComponent.UpdateContact(contact, authenticationData.ApiRootUri,
                                                                authenticationData.AccountContactListsUri, fullUpdate);

            try
            {
                // put the Atom entry at specified Uri
                PutInformation(authenticationData, new Uri(completeUri), data.ToString());
            }
            catch (Exception e)
            {
                if (string.Compare(e.Message, WebExceptionCode404Message) == 0)
                {
                    throw new ConstantException(String.Format(CultureInfo.InvariantCulture,
                                                              "Contact with Id '{0}' does not exist.", contact.Id));
                }
                if (string.Compare(e.Message, WebExceptionCode403Message) == 0)
                {
                    throw new ConstantException("Contact cannot be updated. It belongs to the Do-Not-Mail list.");
                }

                throw new ConstantException(e.Message, e);
            }
        }

        /// <summary>
        /// PUT the data at the specified Uri address. 
        /// Constant Contact server will not send any response
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="address">Uri address</param>
        /// <param name="data">Data to be send at specified Uri address</param>        
        private static void PutInformation(AuthenticationData authenticationData, Uri address, string data)
        {
            // set the Http request content type
            const string contentType = @"application/atom+xml";

            // send a Http PUT request and return the response Stream
            GetResponseStream(authenticationData, address, WebRequestMethods.Http.Put, contentType, data);
        }
        #endregion

        #region Contact - Unsubscribe -
        /// <summary>
        /// Opting-out ("Unsubscribe") a Contact
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="contactId">Contact Id</param>
        /// <exception cref="ArgumentException">Thrown if Id of specified Contact is null or empty</exception>        
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server or if no Contact with specified Id exists
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <remarks>Opted-out Contacts become members of the Do-Not-Mail special list</remarks>
        public static void UnsubscribeContact(AuthenticationData authenticationData, string contactId)
        {
            ValidateAuthenticationData(authenticationData);

            if (string.IsNullOrEmpty(contactId))
            {
                throw new ArgumentException("Contact Id cannot be null or empty", "contactId");
            }

            // create the URI for specified Contact List Id
            string completeUri = String.Format(CultureInfo.InvariantCulture, "{0}/{1}",
                                               authenticationData.AccountContactsUri, contactId);

            try
            {
                // issue a Http DELETE and specified Uri            
                DeleteInformation(authenticationData, new Uri(completeUri));
            }
            catch (Exception e)
            {
                // possible that the Contact does not exist any more
                if (string.Compare(e.Message, WebExceptionCode404Message) == 0)
                {
                    throw new ConstantException(String.Format(CultureInfo.InvariantCulture,
                                                              "Contact with Id '{0}' does not exist.", contactId));
                }

                throw new ConstantException(e.Message, e);
            }
        }

        /// <summary>
        /// Sends a Http DELETE request at the specified Uri address
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="address">Uri address</param>
        private static void DeleteInformation(AuthenticationData authenticationData, Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Credentials = CreateCredentialCache(address, authenticationData);

            request.Method = "DELETE";

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(String.Format(CultureInfo.InvariantCulture,
                                                "Method {0}, response description: {1}", request.Method,
                                                response.StatusDescription));
            }
            finally
            {
                if (response != null)
                {
                    // close the response
                    response.Close();
                }
            }
        }
        #endregion

        #region Contact - Remove from all lists -
        /// <summary>
        /// Remove Contact from all Contact Lists
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="contactId">Contact Id</param>
        /// <exception cref="ArgumentException">Thrown if Id of specified Contact is null or empty</exception>        
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server or if no Contact with specified Id exists
        /// or if ApiKey, Username or Password are null or empty</exception>
        public static void RemoveContactFromAllLists(AuthenticationData authenticationData, string contactId)
        {
            ValidateAuthenticationData(authenticationData);

            if (string.IsNullOrEmpty(contactId))
            {
                throw new ArgumentException("Contact Id cannot be null or empty", "contactId");
            }

            // create the URI for specified Contact Id
            string completeUri = String.Format(CultureInfo.InvariantCulture, "{0}/{1}",
                                               authenticationData.AccountContactsUri, contactId);

            // get Contact by Id
            Contact contact = GetContactDetailsById(authenticationData, contactId);

            // consider that Contact does not needs to be updated
            bool needUpdate = false;

            if (contact.ContactLists.Count != 0)
            {
                // remove Contact from all Contact Lists
                contact.ContactLists.Clear();

                // Contact must be updated
                needUpdate = true;
            }

            if (!needUpdate)
            {
                // no need to update Contact
                return;
            }

            // get the Atom entry for specified Contact
            StringBuilder data = ContactComponent.RemoveContactFromAllLists(contact,
                                                                            authenticationData.AccountContactsUri);

            try
            {
                // put the Atom entry at specified Uri
                PutInformation(authenticationData, new Uri(completeUri), data.ToString());
            }
            catch (Exception e)
            {
                // possible that the Contact does not exist any more; not sure if this could happened
                if (string.Compare(e.Message, WebExceptionCode404Message) == 0)
                {
                    throw new ConstantException(String.Format(CultureInfo.InvariantCulture,
                                                              "Contact with Id '{0}' does not exist.", contactId));
                }

                throw new ConstantException(e.Message, e);
            }
        }
        #endregion


        #region Contact List - Retrieve entire collection -
        /// <summary>
        /// Retrieves the first chunk collection of user Contact Lists that the server provides 
        /// for current Contact Account Owner.
        /// The collection is sorted by the Sort Order and it will not include the system 
        /// predefined lists ("Active", "Removed", "DoNotEmail")
        /// </summary>
        /// <remarks>Constant Contact server provides paged collections</remarks>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="nextChunkId">Link to the next chunk data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>        
        /// <returns>The collection of user Contact Lists</returns>
        public static IList<ContactList> GetUserContactListCollection(AuthenticationData authenticationData, out string nextChunkId)
        {
            ValidateAuthenticationData(authenticationData);

            return GetUserContactListCollection(authenticationData, null, out nextChunkId);
        }

        /// <summary>
        /// Retrieves the collection of user Contact Lists returned by the server at current chunk Id.        
        /// The collection is sorted by the Sort Order and it will not include the system 
        /// predefined lists ("Active", "Removed", "DoNotEmail")
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="currentChunkId">Link to the current chunk data</param>
        /// <param name="nextChunkId">Link to the next chunk of data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>The collection of user Contact Lists</returns>
        public static IList<ContactList> GetUserContactListCollection(AuthenticationData authenticationData, string currentChunkId, out string nextChunkId)
        {
            // get the collection of Contact Lists
            IList<ContactList> list = GetContactListCollection(authenticationData, currentChunkId, out nextChunkId);

            IList<ContactList> nonSystemList = new List<ContactList>();

            foreach (ContactList contactList in list)
            {
                if (!contactList.IsSystemList)
                {
                    nonSystemList.Add(contactList);
                }
            }

            return nonSystemList;
        }

        /// <summary>
        /// Retrieves the collection of Contact Lists returned by the server at current chunk Id.
        /// The collection is sorted by the Sort Order and it will include the system 
        /// predefined lists ("Active", "Removed", "DoNotEmail")
        /// </summary>
        /// <remarks>Constant Contact server provides paged collections</remarks>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="currentChunkId">Link to the current chunk data</param>
        /// <param name="nextChunkId">Link to the next chunk of data</param>
        /// <exception cref="ConstantException">Thrown if communication error with Constant server occur 
        /// or other related with the response from server 
        /// or if ApiKey, Username or Password are null or empty</exception>
        /// <returns>The collection of Contact Lists</returns>
        private static IList<ContactList> GetContactListCollection(AuthenticationData authenticationData, string currentChunkId, out string nextChunkId)
        {
            string currentAddress = String.Format(CultureInfo.InvariantCulture, "{0}{1}",
                                                  authenticationData.AccountContactListsUri, currentChunkId);

            Stream stream = Stream.Null;
            try
            {
                // get the response stream
                stream = GetResponseStream(new Uri(currentAddress), authenticationData);

                // parse the stream and get a collection of Contact Lists
                return ContactListComponent.GetContactListsCollection(stream, out nextChunkId);
            }
            catch (Exception e)
            {
                throw new ConstantException(e.Message, e);
            }
            finally
            {
                // close the response stream
                stream.Close();
            }
        }
        #endregion


        #region Common - Read response -
        /// <summary>
        /// Sends a Http request on specified Uri address and returns the response Stream
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="address">Uri address</param>
        /// <param name="requestMethod">Type of Http request</param>
        /// <param name="contentType">Content type of the Http request</param>
        /// <param name="data">Data to be send at specified Uri address</param>
        /// <returns>Response Stream</returns>
        private static Stream GetResponseStream(AuthenticationData authenticationData,
            Uri address, string requestMethod, string contentType, string data)
        {
            // get data bytes
            byte[] dataByte = Encoding.ASCII.GetBytes(data);

            // send the request and return the response Stream
            return GetResponseStream(authenticationData, address, requestMethod, contentType, dataByte);
        }

        /// <summary>
        /// Sends a Http request on specified Uri address and returns the response Stream
        /// </summary>
        /// <param name="authenticationData">Authentication data (username, password and API Key)</param>
        /// <param name="address">Uri address</param>
        /// <param name="requestMethod">Type of Http request</param>
        /// <param name="contentType">Content type of the Http request</param>
        /// <param name="data">Data to be send at specified Uri address</param>
        /// <returns>Response Stream</returns>
        private static Stream GetResponseStream(AuthenticationData authenticationData,
            Uri address, string requestMethod, string contentType, byte[] data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Credentials = CreateCredentialCache(address, authenticationData);

            request.Method = requestMethod;
            // set the content type of the data being posted
            request.ContentType = contentType;
            // request MUST include a WWW-Authenticate
            request.PreAuthenticate = true;

            // set the content length of the data being posted
            request.ContentLength = data.Length;

            // write data
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);

            Stream responseStream = Stream.Null;
            try
            {
                // get the response Stream
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(String.Format(CultureInfo.InvariantCulture,
                                                "Method {0}, response description: {1}", request.Method,
                                                response.StatusDescription));

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    // server don't send any response to us
                    return Stream.Null;
                }

                // get the response Stream
                responseStream = response.GetResponseStream();

                // read the response stream and save it into a memory stream
                MemoryStream memoryStream = ReadResponseStream(responseStream);

                return memoryStream;
            }
            finally
            {
                // close the Stream object
                stream.Close();

                // close response stream; it also closes the web response
                responseStream.Close();
            }
        }

        /// <summary>
        /// Converts the specified Stream into a Memory Stream
        /// </summary>
        /// <param name="stream">Stream to be converted</param>
        /// <exception cref="IOException">Thrown if any of the underlying IO calls fail</exception>
        /// <returns>Result of conversion</returns>
        private static MemoryStream ReadResponseStream(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();

            // read the stream
            byte[] responseBytes = ReadFully(stream, 0);

            // write all the bytes into the memory stream
            memoryStream.Write(responseBytes, 0, responseBytes.Length);

            // set current position to 0
            memoryStream.Position = 0;

            return memoryStream;
        }

        /// <summary>
        /// Sends a Http GET request and returns the response Stream from the specified Uri address
        /// </summary>
        /// <param name="address">Uri address</param>     
        /// <param name="authenticationData">Authentication data</param>
        /// <returns>Response Stream</returns>
        private static Stream GetResponseStream(Uri address, AuthenticationData authenticationData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Credentials = CreateCredentialCache(address, authenticationData);
            request.Method = WebRequestMethods.Http.Get;

            // request MUST include a WWW-Authenticate
            request.PreAuthenticate = true;

            Stream stream = Stream.Null;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // get the response Stream
                stream = response.GetResponseStream();

                // read the response stream and save it into a memory stream
                return ReadResponseStream(stream);
            }
            catch (WebException e)
            {
                if (null != e.Response)
                {
                    Console.Out.WriteLine("WebException Response Headers =");
                    Console.Out.WriteLine(e.Response.Headers);
                }
                throw;
            }
            finally
            {
                if (stream != Stream.Null)
                {
                    // close response stream; it also closes the web response
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. 
        /// The data is returned as a byte array 
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        /// <exception cref="IOException">Thrown if any of the underlying IO calls fail</exception>
        /// <returns>Stream bytes</returns>
        private static byte[] ReadFully(Stream stream, int initialLength)
        {
            // if we've been passed an unhelpful initial length, 
            // just use 32K
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // if we've reached the end of our buffer, check to see if there's
                // any more information
                if (read != buffer.Length) continue;
                int nextByte = stream.ReadByte();

                // end of stream? if so, we're done
                if (nextByte == -1)
                {
                    return buffer;
                }

                // resize the buffer, put in the byte we've just
                // read, and continue
                byte[] newBuffer = new byte[buffer.Length * 2];
                Array.Copy(buffer, newBuffer, buffer.Length);
                newBuffer[read] = (byte)nextByte;
                buffer = newBuffer;
                read++;
            }

            // buffer is now too big. shrink it
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
        #endregion

        #region Common - Create network credentials -
        /// <summary>
        /// Create credentials for network transport
        /// </summary>
        /// <param name="address">Uri address</param>
        /// <param name="authenticationData">Authentication data</param>
        /// <returns>The Credentials for specified Uri address</returns>
        private static ICredentials CreateCredentialCache(Uri address, AuthenticationData authenticationData)
        {
            NetworkCredential networkCred = new NetworkCredential(authenticationData.AccountUserName, authenticationData.Password);
            CredentialCache cacheCred = new CredentialCache();
            cacheCred.Add(address, "Basic", networkCred);

            return cacheCred;
        }
        #endregion

        #region Validation
        /// <summary>
        /// Check if API Key, Username and Password are not null or empty        
        /// </summary>
        /// <param name="authenticationData">Authentication data to be validated</param>
        /// <exception cref="ConstantException">Thrown if API Key, Username or Password are null or empty</exception>
        private static void ValidateAuthenticationData(AuthenticationData authenticationData)
        {
            if (string.IsNullOrEmpty(authenticationData.Username))
            {
                throw new ConstantException("Username cannot be null or empty");
            }

            if (string.IsNullOrEmpty(authenticationData.Password))
            {
                throw new ConstantException("Password cannot be null or empty");
            }

            if (string.IsNullOrEmpty(authenticationData.ApiKey))
            {
                throw new ConstantException("API Key cannot be null or empty");
            }
        }

        /// <summary>
        /// Check if e-mail is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            const string strRegex = @"^([a-zA-Z0-9_\-\.~#$%]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            return re.IsMatch(email);
        }
        #endregion

        #region Sorting rules
        /// <summary>
        /// Defines the compare criteria for two Contact List instances
        /// </summary>
        /// <param name="x">Contact List to be compared</param>
        /// <param name="y">Contact List to be compared</param>
        /// <returns></returns>
        public static int CompareContactListsBySortOrder(ContactList x, ContactList y)
        {
            if (x.SortOrder.HasValue && y.SortOrder.HasValue)
            {
                return x.SortOrder.Value.CompareTo(y.SortOrder.Value);
            }

            return 0;
        }
        #endregion
    }

    /// <summary>
    /// General exception type. Could be used when communication errors
    /// with the Constant Contact Server occur or other errors
    /// 
    /// </summary>
    [Serializable]
    public class ConstantException : Exception
    {
        #region Constructor
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public ConstantException()
        {
        }

        /// <summary>
        /// Constructor with message parameter
        /// </summary>
        /// <param name="message">Exception message</param>
        public ConstantException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with message and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ConstantException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        #endregion
    }

    /// <summary>
    /// Exception class for Constant Contact Account authentication
    /// 
    /// </summary>
    [Serializable]
    public class ConstantAuthenticationException : Exception
    {
        #region Fields
        /// <summary>
        /// Constant Contact Account username
        /// </summary>
        private string _username;

        /// <summary>
        /// Constant Contact Account password
        /// </summary>
        private string _password;
        #endregion

        #region Constructor
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public ConstantAuthenticationException()
        {
        }

        /// <summary>
        /// Constructor with message parameter
        /// </summary>
        /// <param name="message">Exception mesage</param>
        public ConstantAuthenticationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with message parameter and username
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="username">Constant username</param>
        public ConstantAuthenticationException(string message, string username)
            : base(message)
        {
            Username = username;
        }

        /// <summary>
        /// Constructor with message parameter, username and password
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="username">Constant username</param>
        /// <param name="password">Constant password</param>
        public ConstantAuthenticationException(string message, string username, string password)
            : base(message)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Constructor with message and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ConstantAuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor with message, inner exception and username
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        /// <param name="username">Constant username</param>        
        public ConstantAuthenticationException(string message, Exception innerException, string username)
            : base(message, innerException)
        {
            Username = username;
        }

        /// <summary>
        /// Constructor with message, inner exception, username and password
        /// </summary>
        /// <param name="message">Excetion message</param>
        /// <param name="innerException">Inner exception</param>
        /// <param name="username">Constant username</param>
        /// <param name="password">Constant password</param>
        public ConstantAuthenticationException(string message, Exception innerException, string username, string password)
            : base(message, innerException)
        {
            Username = username;
            Password = password;
        }
        #endregion

        #region Serialization
        /// <summary>
        /// Serialization constructor
        /// </summary>
        /// <param name="serializationInfo">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected ConstantAuthenticationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// GetObjectData override
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        [System.Security.Permissions.SecurityPermission
            (System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Username", _username);
            info.AddValue("Password", _password);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Constant Contact Account username that cannot access Constant resources
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// Gets or sets the Constant Contact Account password that cannot access Constant resources
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        #endregion
    }
}