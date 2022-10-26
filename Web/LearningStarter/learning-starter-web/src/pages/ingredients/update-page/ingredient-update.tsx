import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Input } from "semantic-ui-react";
import { ApiResponse, IngredientGetDto, IngredientUpdateDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./ingredient-update.css";

export const IngredientUpdatePage = () => {
    const history = useHistory();
    let match = useRouteMatch<{id: string}>();
    const id = match.params.id;
    const [ingredient, setIngredients] = useState<IngredientGetDto>();

    useEffect(() => {
        const fetchIngredients = async () => {
        const response = await axios.get<ApiResponse<IngredientGetDto>>(
            `/api/ingredients/${id}`
        );

        if (response.data.hasErrors){
            console.log(response.data.errors);
            return;
        }

        setIngredients(response.data.data);
        }
    
        fetchIngredients();
    }, [id]);

    const onSubmit = async (values: IngredientUpdateDto) => {
        const response = await axios.put<ApiResponse<IngredientGetDto>>(
            `/api/ingredients/${id}`,
            values
        );
        
            if (response.data.hasErrors) {
                response.data.errors.forEach((err) => {
                console.log(err.message);
                });
            } else {
                history.push(routes.ingredients.listing);
            }
    };
    
    return (
        <>
        
        {ingredient &&   (
            <Formik initialValues={ingredient} onSubmit={onSubmit}>
                <Form>
                    <div className="ingredient-update-container">
                        <label htmlFor="name">Name</label>
                    </div>
                    <div className="ingredient-update-container">
                    <Field id="name" name="name">
                        {({field}) => <Input {...field} />}
                    </Field>
                    </div>
                    <div className="ingredient-update-container">
                        <label htmlFor="image">Image</label>
                    </div>
                    <div className="ingredient-update-container">
                    <Field id="image" name="image">
                        {({field}) => <Input {...field} />}
                    </Field>
                    </div>
                    <div className="ingredient-update-container">
                    <Button type="submit">Submit</Button>
                    </div>
                </Form>
            </Formik>
        )}
        </>
        );
    
};