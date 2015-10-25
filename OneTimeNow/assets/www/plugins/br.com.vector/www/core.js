cordova.define("br.com.vector.core", function(require, exports, module) { /*global cordova, module*/

module.exports = {
    greet: function (name, successCallback, errorCallback) {
         cordova.exec(successCallback, errorCallback, "Core", "greet", [name]);
     },
    request: function (name, successCallback, errorCallback) {
         cordova.exec(successCallback, errorCallback, "Core", "request", [name]);
     }
};

});
