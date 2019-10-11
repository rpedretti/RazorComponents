import { Modal } from "./Modal";

namespace rpedrettiBlazorComponents {
    export const modal = new Modal();
}

declare global {
    interface Window {
        rpedrettiBlazorComponents: typeof rpedrettiBlazorComponents;
    }
}

window.rpedrettiBlazorComponents = Object.assign(window.rpedrettiBlazorComponents || {}, rpedrettiBlazorComponents);