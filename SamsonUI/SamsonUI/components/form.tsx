import { Button } from "@nextui-org/button"
import { ReactNode } from "react";

type FormProps = {
    children?: ReactNode
    onClick?: () => void
    buttonText?: string;
}

const Form: React.FC<FormProps> = ({children, onClick, buttonText}) => {
    return (
    <form className="flex flex-col space-y-4 w-2/3 items-center">
        {children}
        <Button className="w-full" variant="bordered" onClick={onClick}>{buttonText}</Button>
    </form>
    )
}

export default Form;