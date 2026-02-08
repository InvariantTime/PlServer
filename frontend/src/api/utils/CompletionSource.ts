
export class CompletionSource<T> {

    private _resolver?: (value: T) => void;

    public promise: Promise<T>

    constructor() {
        this.promise = new Promise(resolver => {
            this._resolver = resolver;
        });
    }

    public resolve(value: T) {
        if (this._resolver != null)
            this._resolver(value);
    }
}