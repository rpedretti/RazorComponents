"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const LightSensor_1 = require("./LightSensor");
const Geolocation_1 = require("./Geolocation");
var rpedrettiBlazorSensors;
(function (rpedrettiBlazorSensors) {
    rpedrettiBlazorSensors.lightSensor = new LightSensor_1.LightSensor();
    rpedrettiBlazorSensors.geolocation = new Geolocation_1.Geolocation();
})(rpedrettiBlazorSensors || (rpedrettiBlazorSensors = {}));
window.rpedrettiBlazorSensors = rpedrettiBlazorSensors;
//# sourceMappingURL=InitNamespaces.js.map