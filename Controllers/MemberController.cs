using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Controllers
{
	public class MemberController : ApiController
	{
		DataClasses1DataContext db = new DataClasses1DataContext();

		// GET api/<controller>
		public IEnumerable<tbl_ProductDetail> Get()
		{
			return db.tbl_ProductDetails.ToList().AsEnumerable();
		}

		// GET api/<controller>/5
		public HttpResponseMessage Get(int id)
		{
			var Productdetail = (from a in db.tbl_ProductDetails where a.Id == id select a).FirstOrDefault();
			if (Productdetail != null)
			{

				return Request.CreateResponse(HttpStatusCode.OK, Productdetail);
			}
			else
			{

				return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Product Not Found");
			}
		}



		// POST api/<controller>
		public HttpResponseMessage Post([FromBody] tbl_ProductDetail _ProductDetail1)
		{
			try
			{
				db.tbl_ProductDetails.InsertOnSubmit(_ProductDetail1);
				db.SubmitChanges();
				var msg = Request.CreateResponse(HttpStatusCode.Created, _ProductDetail1);
				msg.Headers.Location = new Uri(Request.RequestUri + _ProductDetail1.Id.ToString());

				return msg;
			}
			catch (Exception ex)
			{

				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}


		// PUT api/<controller>/5
		public HttpResponseMessage Put(int id, [FromBody] tbl_ProductDetail _ProductDetail1)
		{
			var Productdetail = (from a in db.tbl_ProductDetails where a.Id == id select a).FirstOrDefault();
			if (Productdetail != null)
			{
				Productdetail.Name = _ProductDetail1.Name;
				Productdetail.Description = _ProductDetail1.Description;
				Productdetail.Price = _ProductDetail1.Price;
				db.SubmitChanges();
				return Request.CreateResponse(HttpStatusCode.OK, _ProductDetail1);
			}
			else
			{
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Product Not Found");
			}
		}


		// DELETE api/<controller>/5
		public HttpResponseMessage Delete(int id)
		{
			try
			{
				var _DeleteProductdetail = (from a in db.tbl_ProductDetails where a.Id == id select a).FirstOrDefault();

				if (_DeleteProductdetail != null)
				{

					db.tbl_ProductDetails.DeleteOnSubmit(_DeleteProductdetail);
					db.SubmitChanges();
					return Request.CreateResponse(HttpStatusCode.OK, id);
				}
				else
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product Not Found or Invalid " + id.ToString());
				}
			}
			catch (Exception ex)
			{

				//return response error as bad request  with exception message.  
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}

		}
	}
}