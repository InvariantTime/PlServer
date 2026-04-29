import exp from "constants";
import { Failure, Result, Success } from "../utils/Result";

const registerUrl = 'api/users/register';
const loginUrl = 'api/users/login';
const verifyUrl = 'api/users/verify';

export type Auth = ErrorAuth | SuccessAuth;

export type ErrorAuth = {
    type: "error",
    error: string
} 

export type SuccessAuth = {
    type: "success"
}

export async function register(name: string, password: string) : Promise<Auth> {
    
    const query: RequestInit = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'credentials': 'include'
        },
        body: JSON.stringify({
            'name': name,
            'password': password
        })
    };

    const result = await fetch(registerUrl, query);

    if (result.ok) {
        return { type: "success"};
    }

    const value = await result.json();

    return {
        type: "error",
        error: value.error
    }
}

export async function login(name: string, password: string) : Promise<Auth> {
     
    const query: RequestInit = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'credentials': 'include'
        },
        body: JSON.stringify({
            'name': name,
            'password': password
        })
    };

    const result = await fetch(loginUrl, query);

    if (result.ok) {
        return {type: "success"};
    }

    const value = await result.json();

    return { type:"error", error: value.error };
}

export async function verify() : Promise<Result<string>> {
    
    const query = {
        method: "GET"
    }

    try
    {
        const result = await fetch(verifyUrl, query);
        
        if (result.ok) {
            const data = await result.json();
            return Success(data.name);
        }
        else {
            return Failure("");
        }
    }
    catch (e)
    {
        alert(e);
        return Failure("");
    }
}