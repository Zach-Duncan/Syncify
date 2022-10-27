import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Input } from "semantic-ui-react";
import { ApiResponse, ShoppingListCreateDto, ShoppingListGetDto } from "../../../constants/types";
import { useHistory} from 'react-router-dom'
import { routes } from "../../../routes/config";
import { BaseUrl } from "../../../constants/env-cars";
const initialValues: ShoppingListCreateDto = {
    name: "",
};

export const ShoppingListCreatePage = () => {
    const history = useHistory();
    const onSubmit = async (values: ShoppingListCreateDto) => {
        const response = await axios.post<ApiResponse<ShoppingListGetDto>>(`${BaseUrl}/api/shopping-lists`,
        values
        );

        if(response.data.hasErrors){
            response.data.errors.forEach((err) => {
                console.log(err.message);
            });
        } else {
            history.push(routes.shoppingList.listing)
        }
    };
    return (
        <>
        <Formik initialValues= {initialValues} onSubmit={onSubmit}>
            <Form>
                <div>
                    <label htmlFor="name">Name</label>
                </div>
                <Field id='name' name='name'>
                    {({field}) => <Input {...field} />}
                </Field>

                <div>
                    <Button type="submit">
                        Create
                    </Button>
                </div>
            </Form>
        </Formik>
        </>
    );
};