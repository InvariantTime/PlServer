import "./auth.css"

interface Props {
    goToLogin: () => void
}

export const RegisterPanel = ({goToLogin}: Props) => {

    return (
        <div className="flex items-center flex-col justify-center my-5">
            <header>
                <h1 className="text-4xl">
                    Register
                </h1>
            </header>

            <form>
                <div className="inputBlock">
                    <label htmlFor="name">Name</label>
                    <input id="name" name="name" required
                        className="w-full" />
                </div>

                <div className="inputBlock">
                    <label htmlFor="password">Password</label>
                    <input type="password" id="password" name="password" required />
                </div>

                <div className="inputBlock mb-10">
                    <label htmlFor="cpassword">Confirm password</label>
                    <input type="password" id="cpassword" name="cpassword" required />
                </div>

                <button type="submit" className="text-xl font-bold rounded-md 
                        text-emerald-100 bg-emerald-500 p-2
                        hover:bg-emerald-700 w-full">
                    submit
                </button>
            </form>

            <span className="font-bold mt-4">
                Already have an account? <a 
                    onClick={goToLogin} className="text-blue-500 hover:underline cursor-pointer">login here</a>
            </span>
        </div>
    )
}