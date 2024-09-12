/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { createAttendeeFromDiscriminatorValue, createStatusFromDiscriminatorValue, type Attendee, type Status } from '../../../models/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type Parsable, type ParsableFactory, type RequestConfiguration, type RequestInformation, type RequestsMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1/attendees/{id}
 */
export interface AttendeesItemRequestBuilder extends BaseRequestBuilder<AttendeesItemRequestBuilder> {
    /**
     * Get an attendees information for an event
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<Attendee>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     get(requestConfiguration?: RequestConfiguration<object> | undefined) : Promise<Attendee | undefined>;
    /**
     * Get an attendees information for an event
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toGetRequestInformation(requestConfiguration?: RequestConfiguration<object> | undefined) : RequestInformation;
}
/**
 * Uri template for the request builder.
 */
export const AttendeesItemRequestBuilderUriTemplate = "{+baseurl}/v1/attendees/{id}";
/**
 * Metadata for all the requests in the request builder.
 */
export const AttendeesItemRequestBuilderRequestsMetadata: RequestsMetadata = {
    get: {
        uriTemplate: AttendeesItemRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createAttendeeFromDiscriminatorValue,
    },
};
/* tslint:enable */
/* eslint-enable */