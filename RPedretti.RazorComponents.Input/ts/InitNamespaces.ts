import { Suggestbox } from "./SuggestBox";

namespace rpedrettiBlazorComponents {
    export const suggestbox = new Suggestbox();
}

declare global {
    interface Window {
        rpedrettiBlazorComponents: typeof rpedrettiBlazorComponents;
    }
}

window.rpedrettiBlazorComponents = Object.assign(window.rpedrettiBlazorComponents || {}, rpedrettiBlazorComponents);

$(document).on('click', rpedrettiBlazorComponents.suggestbox.clearSelection);