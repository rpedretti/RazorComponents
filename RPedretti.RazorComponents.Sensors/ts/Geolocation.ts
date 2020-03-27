interface CustomError {
    code: number,
    message: string
}

interface CustomPosition {
    coords: Coordinates,
    timestamp: Date
}

export class Geolocation {
    private mapPosition(position: Position): CustomPosition {
        return {
            coords: {
                accuracy: position.coords.accuracy === NaN ? 0 : position.coords.accuracy,
                altitude: position.coords.altitude === NaN ? 0 : position.coords.altitude,
                altitudeAccuracy: position.coords.altitudeAccuracy === NaN ? 0 : position.coords.altitudeAccuracy,
                heading: position.coords.heading === NaN ? 0 : position.coords.heading,
                latitude: position.coords.latitude === NaN ? 0 : position.coords.latitude,
                longitude: position.coords.longitude === NaN ? 0 : position.coords.longitude,
                speed: position.coords.speed === NaN ? 0 : position.coords.speed,
            },
            timestamp: new Date(position.timestamp)
        };
    };

    private mapPositionError(positionError: PositionError): CustomError {
        return {
            code: positionError.code,
            message: positionError.message
        };
    };

    public watchPosition(objectRef: DotNet.DotNetObject): number | undefined {
        if (navigator.geolocation) {
            const id = navigator.geolocation.watchPosition(
                p => objectRef.invokeMethodAsync('WatchPositionResponse', this.mapPosition(p)),
                e => objectRef.invokeMethodAsync('WatchPositionError', this.mapPositionError(e)),
                {
                    enableHighAccuracy: true
                }
            );

            return id;
        } else {
            const error: CustomError = { code: -1, message: "Geolocation not available" }
            objectRef.invokeMethodAsync('WatchPositionError', error)
            null;
        }
    }

    public stopWatchPosition(watchId: number): void {
        navigator.geolocation?.clearWatch(watchId);
    }
}

window.rpedrettiBlazorSensors = window.rpedrettiBlazorSensors || {};
window.rpedrettiBlazorSensors.geolocation = new Geolocation();
