import { HttpErrorResponse } from '@angular/common/http';

type ExtractOptions = {
  defaultMessage: string;
  invalidRequestMessage?: string; // used when status=400 but body is not helpful
};

export function extractBackendWhy(err: unknown, options: ExtractOptions): string {
  const defaultMsg = options.defaultMessage;
  const invalidReqMsg = options.invalidRequestMessage ?? 'Request not valid. Please check the form fields.';

  if (!(err instanceof HttpErrorResponse)) {
    const anyErr = err as any;
    return anyErr?.message ?? defaultMsg;
  }

  // 400: show "why" from body if possible
  if (err.status === 400) {
    const body = err.error;

    // Case 1: plain text body
    if (typeof body === 'string' && body.trim().length > 0) {
      return body;
    }

    // Case 2: JSON body with common shapes
    if (body && typeof body === 'object') {
      // { message: "..." }
      const message = (body as any).message;
      if (typeof message === 'string' && message.trim().length > 0) return message;

      // { error: "..."} { title: "..."} { detail: "..."} { reason: "..."}
      for (const key of ['error', 'title', 'detail', 'reason']) {
        const v = (body as any)[key];
        if (typeof v === 'string' && v.trim().length > 0) return v;
      }

      // { errors: { field: ["msg1","msg2"], otherField: ["msg"] } }
      const errors = (body as any).errors;
      if (errors && typeof errors === 'object') {
        const messages: string[] = [];
        for (const field of Object.keys(errors)) {
          const fieldErrors = errors[field];
          if (Array.isArray(fieldErrors)) {
            for (const m of fieldErrors) {
              if (typeof m === 'string' && m.trim().length > 0) messages.push(m);
            }
          } else if (typeof fieldErrors === 'string' && fieldErrors.trim().length > 0) {
            messages.push(fieldErrors);
          }
        }
        if (messages.length > 0) return messages.join(' ');
      }

      // Unknown object shape: stringify as last resort
      try {
        const s = JSON.stringify(body);
        if (s && s !== '{}' && s !== 'null') return s;
      } catch {
        // ignore
      }
    }

    return invalidReqMsg;
  }

  // Non-400: prefer a readable string if present, else fall back
  if (typeof err.error === 'string' && err.error.trim().length > 0) return err.error;
  return err.message || defaultMsg;
}
