export interface TaskJson<T> {
  result: T;
  id?: number;
  exception?: unknown;
  status?: number;
  isCanceled?: boolean;
  isCompleted?: boolean;
  isCompletedSuccessfully?: boolean;
  creationOptions?: number;
  asyncState?: unknown;
  isFaulted?: boolean;
}