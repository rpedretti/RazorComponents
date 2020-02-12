export class Modal {
    public updateView = (show: boolean, disableScroll: boolean, id: string) => {
        document.body.style.overflow = show && disableScroll ? "hidden" : '';
        if (show) {
            this.lockAriaForSimblings(id);
        } else {
            this.unlockAriaForSimblings(id);
        }
    }

    private lockAriaForSimblings(id: string) {
        var modalRoot = document.querySelector(`#${id}`) as HTMLElement;
        if (modalRoot) {
            var children = modalRoot.parentElement!.children;
            for (var i = 0; i < children.length; i++) {
                var child = children.item(i);
                if (child?.id != id) {
                    child?.setAttribute("aria-hidden", "true");
                }
            }
        }
    }

    private unlockAriaForSimblings(id: string) {
        var modalRoot = document.querySelector(`#${id}`) as HTMLElement;
        if (modalRoot) {
            var children = modalRoot.parentElement!.children;
            for (var i = 0; i < children.length; i++) {
                var child = children.item(i);
                if (child?.id != id) {
                    child?.removeAttribute("aria-hidden");
                }
            }
        }
    }
}

window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.modal = new Modal();
