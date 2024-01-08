using Features;

namespace FunctionalTests;

[CollectionDefinition("FTests")]
public class TestsFixtureCollection : ICollectionFixture<CustomWebApiForTests<Program>>
{
    
}