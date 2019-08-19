export class LightSensor {
    private sensors = new Map();

    public initSensor = (sensorRef, pollTime: number) => {
        if ('AmbientLightSensor' in window) {
            const sensor = new (window as any).AmbientLightSensor({ frequency: pollTime || 1 });
            sensor.onreading = () => {
                sensorRef.invokeMethodAsync('NotifyReading', sensor.illuminance);
            };
            sensor.onerror = (event) => {
                sensorRef.invokeMethodAsync('NotifyAmbientLightError', event.error.message);
            };
            sensor.start();
            this.sensors.set(sensorRef, sensor);
        } else {
            sensorRef.invokeMethodAsync('NotifyAmbientLightError', 'not supported');
        }
    }

    public stopSensor = (sensorRef) => {
        const sensor = this.sensors.get(sensorRef);
        sensor.stop();
    }
}