/*
 * The MIT License
 *
 * Copyright (c) 2008-2013, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
namespace phoenixconsulting.common.logging {
    public enum AuditEventType {
        LOGIN_SUCCESS = 1,
        DIAGNOSTICS_PAGE,
        DIAGNOSTICS_SENT,
        SUBMIT_CC_PAYMENT_FAILED,
        SUBMIT_PAYPAL_PAYMENT_FAILED,
        SUBMIT_CC_PAYMENT_SUCCESS,
        SUBMIT_PAYPAL_PAYMENT_SUCCESS,
        SUBMIT_CC_PAYMENT_REJECTED,
        SUBMIT_PAYPAL_PAYMENT_REJECTED,
        LOGIN_FAILED,
        SKU_SEARCH,
        KEYWORD_SEARCH,
        ACTIVATION_PAGE,
        USER_LOGIN_CREATED_SUCCESS,
        USER_LOGIN_CREATED_FAILURE,
        USER_PASSWORD_CHANGED_SUCCESS,
        USER_PASSWORD_RECOVERED_SUCCESS,
        USER_PASSWORD_RECOVERED_FAILURE,
        USER_PASSWORD_CHANGED_FAILURE,
        MAILSEND_FAILED,
        MAILSEND_SUCCESS,
        ORDER_REFUNDED
    }
}