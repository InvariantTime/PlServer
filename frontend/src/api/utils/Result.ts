
export type Result<T> = FailureResult | SuccessResult<T>;

type FailureResult = {
    state: "failure",
    error: string
}

type SuccessResult<T> = {
    state: "success",
    value: T
}

export const Failure = (error: string) : FailureResult => { 
    return {state: "failure", error: error};
}

export const Success = <T>(value: T) : SuccessResult<T> => {
    return {state: "success", value: value};
};