using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass]
    public class ApiRequestLockTests
    {
        [TestMethod]
        public void GetLockObjectTest()
        {
            var emptyParam = new Dictionary<string, string>();
            var ticker1Param = new Dictionary<string, string>();
            ticker1Param["ticker"] = "ticker1";
            var ticker2Param = new Dictionary<string, string>();
            ticker2Param["ticker"] = "ticker2";

            var endpointAtickerEmptyReq = new ApiGetRequest("A", emptyParam);
            var endpointAticker1Req = new ApiGetRequest("A", ticker1Param);
            var endpointAticker2Req = new ApiGetRequest("A", ticker2Param);

            var endpointBtickerEmptyReq = new ApiGetRequest("B", emptyParam);
            var endpointBticker1Req = new ApiGetRequest("B", ticker1Param);
            var endpointBticker2Req = new ApiGetRequest("B", ticker2Param);

            Assert.AreEqual(ApiRequestLock.GetLockObject(endpointAtickerEmptyReq), ApiRequestLock.GetLockObject(endpointAtickerEmptyReq));
            Assert.AreEqual(ApiRequestLock.GetLockObject(endpointAticker1Req), ApiRequestLock.GetLockObject(endpointAticker1Req));
            Assert.AreEqual(ApiRequestLock.GetLockObject(endpointAticker2Req), ApiRequestLock.GetLockObject(endpointAticker2Req));

            Assert.AreNotEqual(ApiRequestLock.GetLockObject(endpointAtickerEmptyReq), ApiRequestLock.GetLockObject(endpointAticker1Req));
            Assert.AreNotEqual(ApiRequestLock.GetLockObject(endpointAtickerEmptyReq), ApiRequestLock.GetLockObject(endpointAticker2Req));
            Assert.AreNotEqual(ApiRequestLock.GetLockObject(endpointAticker1Req), ApiRequestLock.GetLockObject(endpointAticker2Req));

            Assert.AreNotEqual(ApiRequestLock.GetLockObject(endpointAtickerEmptyReq), ApiRequestLock.GetLockObject(endpointBtickerEmptyReq));
            Assert.AreNotEqual(ApiRequestLock.GetLockObject(endpointAtickerEmptyReq), ApiRequestLock.GetLockObject(endpointBticker1Req));
            Assert.AreNotEqual(ApiRequestLock.GetLockObject(endpointAticker1Req), ApiRequestLock.GetLockObject(endpointBticker2Req));

        }
    }
}