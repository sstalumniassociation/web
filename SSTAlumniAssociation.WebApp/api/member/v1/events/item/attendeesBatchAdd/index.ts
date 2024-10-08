/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { createEventFromDiscriminatorValue, createStatusFromDiscriminatorValue, type Event, type Status } from '../../../../models/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type Parsable, type ParsableFactory, type RequestConfiguration, type RequestInformation, type RequestsMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1/events/{id}/attendees:batchAdd
 */
export interface AttendeesBatchAddRequestBuilder extends BaseRequestBuilder<AttendeesBatchAddRequestBuilder> {
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<Event>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     post(requestConfiguration?: RequestConfiguration<AttendeesBatchAddRequestBuilderPostQueryParameters> | undefined) : Promise<Event | undefined>;
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toPostRequestInformation(requestConfiguration?: RequestConfiguration<AttendeesBatchAddRequestBuilderPostQueryParameters> | undefined) : RequestInformation;
}
export interface AttendeesBatchAddRequestBuilderPostQueryParameters {
    users?: string;
}
/**
 * Uri template for the request builder.
 */
export const AttendeesBatchAddRequestBuilderUriTemplate = "{+baseurl}/v1/events/{id}/attendees:batchAdd{?users*}";
/**
 * Metadata for all the requests in the request builder.
 */
export const AttendeesBatchAddRequestBuilderRequestsMetadata: RequestsMetadata = {
    post: {
        uriTemplate: AttendeesBatchAddRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createEventFromDiscriminatorValue,
    },
};
/* tslint:enable */
/* eslint-enable */
