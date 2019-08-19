"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class LightSensor {
    constructor() {
        this.sensors = new Map();
        this.initSensor = (sensorRef, pollTime) => {
            if ('AmbientLightSensor' in window) {
                const sensor = new window.AmbientLightSensor({ frequency: pollTime || 1 });
                sensor.onreading = () => {
                    sensorRef.invokeMethodAsync('NotifyReading', sensor.illuminance);
                };
                sensor.onerror = (event) => {
                    sensorRef.invokeMethodAsync('NotifyAmbientLightError', event.error.message);
                };
                sensor.start();
                this.sensors.set(sensorRef, sensor);
            }
            else {
                sensorRef.invokeMethodAsync('NotifyAmbientLightError', 'not supported');
            }
        };
        this.stopSensor = (sensorRef) => {
            const sensor = this.sensors.get(sensorRef);
            sensor.stop();
        };
    }
}
exports.LightSensor = LightSensor;
//# sourceMappingURL=LightSensor.js.map