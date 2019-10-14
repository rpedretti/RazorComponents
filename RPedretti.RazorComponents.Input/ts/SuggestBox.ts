import { Helpers } from "./Helpers";

export class Suggestbox {

    private readonly registeredSuggestboxes = new Map<string, any>();
    private readonly suggestboxesToClear = new Map<string, boolean>();

    public setSuggestion = (id: string) => {
        this.suggestboxesToClear.set(id, false);
        Helpers.focusById(id);
    }

    public unregisterSuggestBox = (id) => {
        this.registeredSuggestboxes.delete(id);
    }

    public initSuggestBox = (dotnetRef, inputId: string) => {
        this.registeredSuggestboxes.set(inputId, dotnetRef);
        this.suggestboxesToClear.set(inputId, false);
        const element = $(`#${inputId}`);
        element.keydown($event => {

            var parent = $($event!!.target!!.parentNode!!.parentNode!!);
            if (parent.hasClass('-open') && ($event.key === 'ArrowDown' || $event.key === 'ArrowUp')) {
                $event.preventDefault();
            }

            if ($event.key !== 'ArrowDown' &&
                $event.key !== 'ArrowUp' &&
                $event.key !== 'Enter' &&
                $event.key !== 'Escape' &&
                $event.key !== 'Tab') {
                this.suggestboxesToClear.set(inputId, true);
                $event.stopPropagation();
            }
        });

        return 1;
    }

    public clearSelection = (e) => {
        for (var [id, ref] of this.registeredSuggestboxes) {
            if (this.suggestboxesToClear.get(id)) {
                const element = $(`#${id}`);
                if (Helpers.senseClickOutside($(e.target), element)) {
                    ref.invokeMethodAsync('ClearSelection');
                    this.suggestboxesToClear.set(id, false);
                    return;
                }
            }
        }
    }
}

window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.suggestbox = new Suggestbox();

$(document).on('click', window.rpedrettiBlazorComponents.suggestbox.clearSelection);