import "../update-page/events-update.css";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import { Button, Dropdown, Header, Input } from "semantic-ui-react";
import React, { useState, useEffect } from "react";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import {
  ApiResponse,
  EventGetDto,
  EventUpdateDto,
  OptionDto,
} from "../../../constants/types";
import toast from "react-hot-toast";

export const EventUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [event, setEvent] = useState<EventGetDto>();
  const [calendarOptions, setCalendarOptions] = useState<OptionDto[]>();
  console.log("debug", calendarOptions);

  useEffect(() => {
    const fetchEvent = async () => {
      const response = await axios.get<ApiResponse<EventGetDto>>(
        `/api/events/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setEvent(response.data.data);
    };

    fetchEvent();
  }, [id]);

  useEffect(() => {
    async function getCalendarOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/calendars/options"
      );

      setCalendarOptions(response.data.data);
    }

    getCalendarOptions();
  }, []);

  const onSubmit = async (values: EventUpdateDto) => {
    const response = await axios.put<ApiResponse<EventGetDto>>(
      `/api/events/${id}`,
      values
    );

    if (response.data.hasErrors) {
      toast.error("Error Occured, please try again", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.events.listing);
      toast.success("Event successfully updated", {
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
      {event && (
        <Formik initialValues={event} onSubmit={onSubmit}>
          <Form>
            <div className="events-update-container">
              <Header>Update Event</Header>
            </div>
            <div className="events-update-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="events-update-container">
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-update-container">
              <label htmlFor="eventDetails">Event Details</label>
            </div>
            <div className="events-update-container">
              <Field id="eventDetails" name="eventDetails">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-update-container">
              <label htmlFor="startDate">Start Date</label>
            </div>
            <div className="events-update-container">
              <Field id="startDate" name="startDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
            </div>
            <div className="events-update-container">
              <label htmlFor="endDate">End Date</label>
            </div>
            <div className="events-update-container">
              <Field id="endDate" name="endDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
            </div>
            <div className="events-update-container">
              <label htmlFor="calendar">Calendar</label>
            </div>
            <div className="events-update-container">
              <Field name="calendarId" id="calendarId" className="field">
                {({ field, form }) => (
                  <Dropdown
                    selection
                    options={calendarOptions}
                    {...field}
                    onChange={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                    onBlur={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                  />
                )}
              </Field>
            </div>
            <div className="events-update-container">
              <Button
                positive
                icon="check"
                content="Update"
                labelPosition="left"
                type="submit"
              />
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
