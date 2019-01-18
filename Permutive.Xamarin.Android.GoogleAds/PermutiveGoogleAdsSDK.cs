using Android.Content;

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

}