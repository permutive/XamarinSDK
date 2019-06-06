using Android.Content;
using Android.Gms.Ads.DoubleClick;
using Com.Permutive.Android.Ads;

namespace Permutive.Xamarin
{

    public class AliasAaidProvider : IAliasProvider
    {
        private Com.Permutive.Android.Aaid.AaidAliasProvider provider;

        public AliasAaidProvider(Context context)
        {
            this.provider = new Com.Permutive.Android.Aaid.AaidAliasProvider(context);
        }

        public object InternalProvider()
        {
            return provider;
        }
    }


    public static class AdRequestExtensions
    {

        public static PublisherAdRequest.Builder AddPermutiveTargeting(this PublisherAdRequest.Builder builder, PermutiveSdk permutive)
        {
            if (permutive is PermutiveAndroid permutiveImpl)
            {
                return PublisherAdRequestUtils.AddPermutiveTargeting(builder, permutiveImpl.InternalPermutive());
            }
            return builder;
        }

        public static PublisherAdRequest BuildWithPermutiveTargeting(this PublisherAdRequest.Builder builder, PermutiveSdk permutive)
        {
            if (permutive is PermutiveAndroid permutiveImpl)
            {
                return PublisherAdRequestUtils.AddPermutiveTargeting(builder, permutiveImpl.InternalPermutive()).Build();
            }
            return builder.Build();
        }
    }

}