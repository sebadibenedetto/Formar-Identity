using AutoFixture;
using AutoFixture.AutoMoq;

namespace Identity.Infrastructure.Test.Customizations
{
    internal class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization()
            : base(new AutoMoqCustomization())
        { }
    }
}