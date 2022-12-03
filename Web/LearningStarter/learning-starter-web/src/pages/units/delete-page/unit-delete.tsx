import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Header, Input } from "semantic-ui-react";
import { ApiResponse, UnitGetDto } from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./unit-delete.css";
import toast from "react-hot-toast";

export const UnitDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [unit, setUnits] = useState<UnitGetDto>();

  useEffect(() => {
    const fetchUnits = async () => {
      const response = await axios.get<ApiResponse<UnitGetDto>>(
        `/api/units/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setUnits(response.data.data);
    };

    fetchUnits();
  }, [id]);

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<UnitGetDto>>(
      `/api/units/${id}`
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.units.listing);
      toast.success("Unit deleted", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };
  return (
    <>
      {unit && (
        <Formik initialValues={unit} onSubmit={onSubmit}>
          <div className="unit-delete-container">
            <Form>
              <div className="unit-delete-container">
                <Header>Delete Unit</Header>
              </div>
              <div className="unit-delete-container">
                <label htmlFor="name">Name</label>
              </div>
              <div className="unit-delete-container">
                <Field id="name" name="name">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>
              <div className="unit-delete-container">
                <label htmlFor="name">Abbreviation</label>
              </div>
              <div className="unit-delete-container">
                <Field id="abbreviation" name="abbreviation">
                  {({ field }) => <Input {...field} />}
                </Field>
              </div>
              <div className="unit-delete-container">
                <Button
                  negative
                  icon="trash"
                  content="Delete"
                  labelPosition="left"
                  type="submit"
                />
              </div>
            </Form>
          </div>
        </Formik>
      )}
    </>
  );
};
