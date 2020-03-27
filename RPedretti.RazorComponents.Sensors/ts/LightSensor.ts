export class LightSensor {
    private sensors = new Map<DotNet.DotNetObject, any>();

    public initSensor = async (sensorRef, pollTime: number) => {
        try {
            const { state } = await navigator.permissions.query({ name: 'ambient-light-sensor' });
            if ('AmbientLightSensor' in window) {

                if (state === 'granted') {
                    const sensor = new window['AmbientLightSensor']({ frequency: pollTime || 1 });
                    sensor.onreading = () => {
                        sensorRef.invokeMethodAsync('NotifyReading', sensor.illuminance);
                    };
                    sensor.onerror = (event) => {
                        sensorRef.invokeMethodAsync('NotifyAmbientLightError', event.error.message);
                    };
                    sensor.start();
                    this.sensors.set(sensorRef, sensor);
                } else if ('ondevicelight' in window) {
                    window.addEventListener('devicelight', e => {
                        sensorRef.invokeMethodAsync('NotifyReading', e.value);
                    });
                } else {
                    sensorRef.invokeMethodAsync('NotifyAmbientLightError', 'not supported');
                }
            }
        } catch (e) {
            sensorRef.invokeMethodAsync('NotifyAmbientLightError', e.message);
        }
    }

    public stopSensor(sensorRef: DotNet.DotNetObject) {
        const sensor = this.sensors.get(sensorRef);
        sensor?.stop();
    }
}

window.rpedrettiBlazorSensors = window.rpedrettiBlazorSensors || {};
window.rpedrettiBlazorSensors.lightSensor = new LightSensor();