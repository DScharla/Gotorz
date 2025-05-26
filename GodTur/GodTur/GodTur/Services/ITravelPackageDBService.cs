using GodTur.Models;
using Shared;

namespace GodTur.Services
{
	public interface ITravelPackageDBService
	{
		Task<TravelPackage> CreateTravelPackageAsync(TravelPackageDTO dto);
	}
}