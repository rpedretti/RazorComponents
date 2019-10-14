import { Modal } from "./Modal";

declare global {
    interface Window {
        rpedrettiBlazorComponents: {
            modal: Modal
        }
    }
}
