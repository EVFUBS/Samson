import { Button } from "@nextui-org/button"
import { ReactNode } from "react";

type FormProps = {
    children?: ReactNode
    onSave?: () => void
}

const Form: React.FC<FormProps> = ({children, onSave}) => {
    return (
    <form className="flex flex-col space-y-4 w-2/3 items-center">
        {children}
        <Button className="w-full" variant="bordered" onClick={onSave}>Save</Button>
    </form>
    )
}

export default Form;