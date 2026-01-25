import { environment } from '../../../enviroment';

export type DebugLevel = 'debug' | 'info' | 'warn' | 'error';

type DebugOptions = {
  enabled?: boolean;
  prefix?: string;
};

export class DebugUtil {
  private static isEnabled(explicit?: boolean): boolean {
    if (typeof explicit === 'boolean') return explicit;
    return !environment.production;
  }

  private static format(prefix: string | undefined, level: DebugLevel, args: any[]): any[] {
    const tag = prefix ? `[${prefix}]` : '[DEBUG]';
    return [tag, level.toUpperCase(), ...args];
  }

  static debug(prefix: string, ...args: any[]): void {
    if (!this.isEnabled()) return;
    console.debug(...this.format(prefix, 'debug', args));
  }

  static info(prefix: string, ...args: any[]): void {
    if (!this.isEnabled()) return;
    console.info(...this.format(prefix, 'info', args));
  }

  static warn(prefix: string, ...args: any[]): void {
    if (!this.isEnabled()) return;
    console.warn(...this.format(prefix, 'warn', args));
  }

  static error(prefix: string, ...args: any[]): void {
    if (!this.isEnabled()) return;
    console.error(...this.format(prefix, 'error', args));
  }
}
