import { Suggestbox } from "./SuggestBox";

declare global {
    interface Window {
        rpedrettiBlazorComponents: {
            suggestbox: Suggestbox
        }
    }
}
