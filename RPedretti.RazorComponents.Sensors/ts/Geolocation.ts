interface CustomError {
    code: number,
    message: string
}

interface CustomPosition {
    coords: {
        accuracy: number,
        altitude: number | null,
        altitudeAccuracy: number | null,
        heading: number | null,
        latitude: number,
        longitude: number,
        speed: number | null
    },
    timestamp: Date
}

export class Geolocation {
    private mapPosition(position: Position): CustomPosition {
        return {
            coords: position.coords,
            timestamp: new Date(position.timestamp)
        };
    };

    private mapPositionError(positionError: PositionError): CustomError {
        return {
            code: positionError.code,
            message: positionError.message
        };
    };

    watchPosition = (objectRef): number | undefined => {
        if (navigator.geolocation) {
            const id = navigator.geolocation.watchPosition(
                p => objectRef.invokeMethodAsync('WatchPositionResponse', this.mapPosition(p)),
                e => objectRef.invokeMethodAsync('WatchPositionError', this.mapPositionError(e))
            );

            return id;
        } else {
            const error: CustomError = { code: -1, message: "Geolocation not available" }
            objectRef.invokeMethodAsync('WatchPositionError', error)
            null
        }
    }

    stopWatchPosition = (watchId: number) => {
        if (navigator.geolocation) {
            navigator.geolocation.clearWatch(watchId);
        }
    }
}

window.rpedrettiBlazorSensors = window.rpedrettiBlazorSensors || {};
window.rpedrettiBlazorSensors.geolocation = new Geolocation();
