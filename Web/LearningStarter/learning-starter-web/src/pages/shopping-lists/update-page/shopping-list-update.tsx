import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import { ApiResponse, ShoppingListGetDto, ShoppingListUpdateDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";

export const ShoppingListUpdatePage = () => {
    const history = useHistory();
    let match = useRouteMatch<{id: string}>();
    const id = match.params.id;
    const [shoppingList, setShoppingList] = useState<ShoppingListGetDto>();

    useEffect(() => {
        const fetchShoppingList = async () => {
        const response = await axios.get<ApiResponse<ShoppingListGetDto>>(
            `/api/shopping-lists/${id}`
        );

        if (response.data.hasErrors){
            console.log(response.data.errors);
            return;
        }

        setShoppingList(response.data.data);
        }
    
        fetchShoppingList();
    }, [id]);

    const onSubmit = async (values: ShoppingListUpdateDto) => {
        const response = await axios.put<ApiResponse<ShoppingListGetDto>>(
            `/api/shopping-lists/${id}`,
            values
        );
        
            if (response.data.hasErrors) {
                response.data.errors.forEach((err) => {
                console.log(err.message);
                });
            } else {
                history.push(routes.shoppingList.listing);
            }
    };
    
    return (
        <>
        
        {shoppingList &&   (
            <Formik initialValues={shoppingList} onSubmit={onSubmit}>
                <Form>
                    <div>
                        <label htmlFor="name">Name</label>
                    </div>
                    <Field id="name" name="name">
                        {({field}) => <Input {...field} />}
                    </Field>
                    <div>
                    <Button type="submit">Submit</Button>
                    </div>
                </Form>
            </Formik>
        )}
        </>
        );
    
};