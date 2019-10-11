export class Modal {
    public setScroll = (scroll: boolean) => {
        document.body.style.overflow = scroll ? "auto" : "hidden";
    }
}