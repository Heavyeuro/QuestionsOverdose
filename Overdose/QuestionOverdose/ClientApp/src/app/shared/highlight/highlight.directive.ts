import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import { GlobalConstants } from "../../common/global-constants";

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective {

  constructor(private el: ElementRef) { }
  defaultColor: string = GlobalConstants.defaultBackgroundColor;
  @Input() makeHighlight: boolean = true;

  @Input('appHighlight') highlightColor: string;

  @HostListener('mouseenter') onMouseEnter() {
    if (this.makeHighlight)
      this.highlight(this.highlightColor || this.defaultColor);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.highlight(null);
  }

  @HostListener('click') onClick() {
    this.highlight(null);
  }

  private highlight(color: string) {
    this.el.nativeElement.style.backgroundColor = color;
  }
}
