"use client"

import Form from "@/components/form";
import { Input } from "@nextui-org/input";

export default function Page() {

	const onSamsonActionClick = () => {
		console.log("SamsonAction")
	}

	return (
		<div className="">
			<div>
				<h3>Wake word detection</h3>
				<Form onClick={onSamsonActionClick} buttonText="Send">
					<Input type="text" label="Enter command for samson to see the output action!" placeholder="Enter command"></Input>
				</Form>
			</div>

			<div>
				<h3>Actions inference</h3>
				<Form onClick={onSamsonActionClick} buttonText="Send">
					<Input type="text" label="Enter command for samson to see the output action!" placeholder="Enter command"></Input>
				</Form>
			</div>
		</div>
	)
}