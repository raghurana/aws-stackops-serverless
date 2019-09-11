using Newtonsoft.Json;
using StackopsCore.Models;
using Xunit;

namespace StackopsCoreUnitTests
{
    public class StackActionRequestTests
    {
        [Fact]
        public void JsonDeserialization_NoStacksSpecified_Success()
        {
            var json = "{\"action\": \"start\"}";
            var request = JsonConvert.DeserializeObject<StackActionRequest>(json);

            Assert.Null(request.Stacks);
            Assert.Equal("start", request.Action);
        }

        [Fact]
        public void JsonDeserilization_StackNamesSpecified_Success()
        {
            var json = "{\"action\": \"start\", \"stacks\" : [ \"stack1\", \"stack2\"] }";
            var request = JsonConvert.DeserializeObject<StackActionRequest>(json);

            Assert.Equal(2, request.Stacks.Length);
            Assert.Equal("stack1", request.Stacks[0]);
            Assert.Equal("stack2", request.Stacks[1]);
            Assert.Equal("start", request.Action);
        }
    }
}