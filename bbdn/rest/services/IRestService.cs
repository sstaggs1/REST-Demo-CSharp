using System.Threading.Tasks;

namespace REST_Demo_CSharp
{
	public interface IRestService<TRestModel> 
	{
		Task<TRestModel> CreateObject (TRestModel T);

		Task<TRestModel> ReadObject ();

		Task<TRestModel> UpdateObject (TRestModel T);

		Task<TRestModel> DeleteObject ();
	}
}

